namespace HelloApi.Models
{
  public class Book
  {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Author {get; set;}
      public string Publisher {get; set;}
      public DateTime PublishDate { get; set; }
      public int Price { get; set; }

  }
}