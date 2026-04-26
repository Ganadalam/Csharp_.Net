// 웹 애플리케이션을 만들기 위한 '빌더(Builder)' 객체를 생성.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();          // 각 기능을 바구니에 담아보자 1) 컨트롤러 기능을 쓸 거야!
builder.Services.AddEndpointsApiExplorer(); // 2) API가 어디 있는지 찾아주는 기능을 넣을게!
builder.Services.AddSwaggerGen();           // 3) Swagger(문서화 도구)를 생성하는 기능을 추가해줘!

var app = builder.Build(); // 4) 조립 = 지금까지 바구니에 담은 설정과 서비스들을 가지고 실제 실행 가능한 '앱(App)' 객체를 만들자. (설계도, 부품 완 -> 자동차 조립 완)

if(app.Environment.IsDevelopment()) // 미들웨어 - 규칙 설정 : 지금 개발 중이면?
{
    app.UseSwagger();   // Swagger 데이터를 만들고
    app.UseSwaggerUI(); // 웹 화면으로 예쁘게 보여줘!
}

app.UseHttpsRedirection();  // http로 들어오면 안전한 https로 자동으로 바꿔줘.
app.UseAuthorization();    // 권한이 있는 사용자인지 체크해.

app.MapControllers(); // 컨트롤러에 적힌 주소들(Route)을 연결해줘.

app.MapGet("/hello", () => "hello from Windows ASP.NET core Web API"); 
// 누군가 주소창에 "/hello"라고 치면 이 문장을 보여줘!

app.Run(); // 서버를 켜고 손님(요청)을 기다리기 시작해.

// https://localhost:5001/hello 접속 시, hello from Windows ASP.NET core Web API 를 볼 수 있음