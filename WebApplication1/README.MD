[스터디] 처음 만나는 C# 기반 학습

📚 Book Management API Service

추가 기록 : https://lunar-halo.tistory.com/57

> **ASP.NET Core 10.0 기반의 도서 관리 백엔드 시스템**
> 로컬 환경부터 Docker 컨테이너라이징, 외부 기기(Mobile) 연동까지 고려한 풀스택 인프라 구축 프로젝트
> (윈도우 기반 -> "맥" 환경 설정, kestrel 바인딩 주소 -> "Docker" 설정 등 차이가 있습니다)

## 🛠 Tech Stack

- **Backend:** .NET 10.0 (C#)
- **Database:** MySQL (Local & Remote access)
- **DevOps:** Docker (Containerization)
- **Tools:** VS Code, MySQL Workbench, Swagger UI

---

## 🚀 Key Features

- **RESTful API:** 도서 정보에 대한 CRUD(Create, Read, Update, Delete) 기능 구현
- **Local-First Architecture:** Docker를 이용한 독립적인 실행 환경 구축
- **Cross-Device Access:** 동일 네트워크 내 모바일 기기에서의 API 호출 및 실시간 데이터 확인

---

## 🔥 Troubleshooting (시행착오 및 해결 과정)

### 1. Docker 컨테이너와 로컬 DB 간의 통신 문제

- **문제:** Docker 컨테이너 내부에서 실행되는 API 서버가 맥북 로컬에 설치된 MySQL에 연결하지 못함 (`MySqlException`).
- **원인:** 컨테이너 내부의 `localhost`는 맥북 본체가 아닌 컨테이너 자기 자신을 가리키기 때문임을 파악.
- **해결:** Connection String의 Server 주소를 **`host.docker.internal`**로 변경하여 컨테이너가 호스트(맥북) IP를 찾아가도록 수정.

### 2. 네트워크 포트 매핑 및 서버 수신 포트 불일치

- **문제:** `docker run -p 8080:80`으로 실행했으나 외부 접속 불가.
- **원인:** 애플리케이션 로그 확인 결과, 서버가 내부적으로 80이 아닌 **8080 포트**에서 Listening 중임을 발견.
- **해결:** 포트 포워딩 설정을 **`-p 8080:8080`**으로 일치시켜 외부 요청이 정확한 내부 포트로 전달되도록 교정.

### 3. 외부 기기(Mobile) 연동 및 CORS/IP 이슈

- **문제:** 폰에서 맥북 IP로 접속 시 데이터가 뜨지 않음.
- **원인:** 1. 폰이 와이파이가 아닌 데이터(LTE)를 사용 중이라 동일 네트워크에 있지 않음. 2. 서버 실행 시 `localhost`가 아닌 `0.0.0.0`으로 호스트를 개방해야 함을 인지.
- **해결:** 폰을 동일 Wi-Fi에 연결하고, 맥북의 **Private IP(192.168.0.5)**를 사용하여 접속 성공.

### 4. MySQL 외부 접속 권한 보안 설정

- **문제:** 네트워크는 뚫렸으나 DB 접속 시 `Access Denied` 에러 발생.
- **원인:** MySQL은 보안상 `root@localhost` 접속만 허용함.
- **해결:** MySQL 명령어를 통해 **`root@'%'`** 계정에 모든 호스트 접속 권한을 부여하고 `FLUSH PRIVILEGES`를 실행하여 외부 컨테이너의 접근을 승인함.

---

## 📂 Project Structure

```text
WebApplication1/
├── Controllers/       # API 라우팅 및 비즈니스 로직 (BookController.cs)
├── Models/            # 데이터 구조 정의 (Books.cs)
├── Properties/        # 실행 설정 (launchSettings.json)
├── Dockerfile         # 컨테이너 빌드 설정 (Multi-stage Build)
└── Program.cs         # 서버 설정 및 의존성 주입
```

---

## 💡 Lessons Learned

- **로그 중심의 디버깅:** `docker logs`와 서버 실행 로그를 통해 직관적인 추측 대신 근거 기반의 문제 해결 습관을 기름.
- **네트워크의 이해:** 공인 IP와 사설 IP의 차이, 그리고 Docker 네트워크 브릿지 구조에 대한 실무적 지식 습득.
- **유연한 환경 대응:** M4 Pro 맥북 환경에서 발생하는 경로 문제(Zsh, Path) 및 아키텍처 차이를 직접 해결하며 환경 적응력 향상.
