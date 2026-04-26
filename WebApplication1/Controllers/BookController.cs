using HelloApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;

namespace HelloApi.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
  {
      private readonly string connectionString = "Server=localhost;Database=books;Uid=root;Pwd=01097059324;";
    [HttpGet]
      public IEnumerable<Book> GetBooks()
    {
        List<Book> list = new List<Book>();

        using (MySqlConnection conn = new MySqlConnection (connectionString))
      {
        conn.Open();

        string sql = "SELECT Id, Title, Author, Publisher, PublishDate, Price FROM bookstore";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while(reader.Read())
          {
              list.Add(new Book
              {
                Id = reader.GetInt32("Id"),
                Title = reader.GetString("Title"),
                Author = reader.GetString("Author"),
                PublishDate = reader.GetDateTime("PublishDate"),
                Publisher = reader.GetString("Publisher"),
                Price = reader.GetInt32("Price")
                
            });
          }
        }

      }
      return list;
    }


    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
      using(MySqlConnection conn = new MySqlConnection(connectionString))
      {
          Console.WriteLine("DB 연결 시도 중..."); // 터미널에 이 글자가 뜨는지 확인
conn.Open();
Console.WriteLine("DB 연결 성공!");    // 이 글자가 안 뜨고 멈춘다면 연결 문제입니다.

          string sql = @"INSERT INTO bookstore
          (Title, Author, Publisher, PublishDate, Price)
          VALUES (@Title, @Author, @Publisher, @PublishDate, @Price)";
          MySqlCommand cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@Title", book.Title);

          cmd.Parameters.AddWithValue("@Author", book.Author);
          cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
          cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
          cmd.Parameters.AddWithValue("@Price", book.Price);

          cmd.ExecuteNonQuery();
      }
      return Ok(new { message = "Book added successfully"});
    }
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book book)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
          conn.Open();

          string sql = @"UPDATE bookstore SET
              Title = @Title,
              Author = @Author,
              Publisher = @Publisher,
              PublishDate = @PublishDate,
              Price = @Price
              WHERE Id = @Id";
            
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
            cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
            cmd.Parameters.AddWithValue("@Price", book.Price);

            int rows = cmd.ExecuteNonQuery();

            if(rows > 0)
              return Ok(new {message = "Book updated successfully"});
            else
              return NotFound(new { message = "Book not found"});
      }
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
          conn.Open();

          string sql = "DELETE FROM bookstore WHERE Id = @Id";

          using (MySqlCommand cmd = new MySqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            if(rows > 0)
                return Ok(new { message = "Book deleted successfully."});
            else
                return NotFound(new {message = "Book not found."});
        }
      }
    }
}
}

