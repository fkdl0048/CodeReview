# FileExecutionProcess

> exe파일 실행과정에 대한 정리

과거 학부시절의 내용과 면접에서 나온 이슈를 정리한 내용이다.

## .exe 파일이란?

> 운영 체제에서 실행 가능한 파일을 나타내는 확장자로 Executable의 줄임말이다.

exe파일은 PE (Portable Executable) 형식의 구조를 가지고 있으며 여러 섹션으로 나누어져 있다.

### DOS 헤더 (MS-DOS Header)

- 파일의 시작 부분에 위치하며, 오래된 DOS 환경과의 호환성을 위해 존재
- MZ로 시작하며, 이는 DOS 실행 파일이 맞다는 것을 의미
- 중요한 필드 중 하나는 e_lfanew로, PE 헤더의 오프셋을 가리킨다.

```c
typedef struct _IMAGE_DOS_HEADER {
    WORD e_magic; // MZ
    WORD e_cblp;
    WORD e_cp;
    WORD e_crlc;
    WORD e_cparhdr;
    WORD e_minalloc;
    WORD e_maxalloc;
    WORD e_ss;
    WORD e_sp;
    WORD e_csum;
    WORD e_ip;
    WORD e_cs;
    WORD e_lfarlc;
    WORD e_ovno;
    WORD e_res[4];
    WORD e_oemid;
    WORD e_oeminfo;
    WORD e_res2[10];
    LONG e_lfanew; // PE 헤더의 오프셋
} IMAGE_DOS_HEADER, *PIMAGE_DOS_HEADER;
```

### DOS Stub

DOS 환경에서 실행될 때 `"This program cannot be run in DOS mode"`라는 메시지를 출력하는 코드가 들어있는 부분이다.

*Windows 환경에서는 무시*

### PE 헤더 (PE Header)

- **"PE\0\0"**라는 서명으로 시작되며, PE 파일 형식임을 나타낸다.
- PE 헤더는 파일의 다양한 속성을 정의하고, 실행에 필요한 정보들을 포함
- PE 헤더 안에는 Machine, NumberOfSections, TimeDateStamp 등의 필드가 포함되어 있다.

### 옵셔널 헤더 (Optional Header)

- 실행 파일의 입출력 관련 정보를 담고 있다.
- 여기에는 진입점(Entry Point) 주소, 이미지 크기, 파일 정렬 정보 등이 포함되어 있다.
- 중요한 필드로는 ImageBase, SectionAlignment, SizeOfImage 등이 있다.

### 섹션 헤더 (Section Headers)

- 파일이 포함하는 코드, 데이터, 리소스 등을 정의하는 여러 섹션들의 메타 정보를 포함
- 각 섹션은 코드 실행, 데이터 저장, 초기화된 데이터와 같이 각기 다른 목적으로 사용
- 일반적으로 .text, .data, .rdata, .rsrc 등의 섹션이 포함

### 섹션 (Sections)

- .text: 실행할 코드가 포함된 섹션으로, 프로그램의 실제 명령어들이 위치합니다.
- .data: 프로그램에서 사용되는 전역 변수 및 초기화된 데이터가 포함된 섹션입니다.
- .rdata: 읽기 전용 데이터(예: 상수, 문자열)가 저장됩니다.
- .rsrc: 프로그램의 리소스(아이콘, 이미지, 문자열 등)를 포함하는 섹션입니다.
- .reloc: 재배치 정보가 포함된 섹션으로, 동적 로딩 시 필요할 수 있습니다.

### 구조 요약

```plaintext
+-----------------+
|   DOS Header    |
+-----------------+
|    DOS Stub     |
+-----------------+
|    PE Header    |
+-----------------+
| Optional Header |
+-----------------+
| Section Headers |
+-----------------+
|    .text        |  <- Code section (Instructions)
+-----------------+
|    .data        |  <- Data section (Global variables)
+-----------------+
|    .rdata       |  <- Read-only data section (Constants)
+-----------------+
|    .rsrc        |  <- Resources (Icons, Images)
+-----------------+
|    .reloc       |  <- Relocation info
+-----------------+
```

여기서 나오는 .text/.data 영역은 [Memory](https://github.com/fkdl0048/CodeReview/blob/main/ComputerScience/Memory/README.md)에서 다루었던 내용과 연관이 있다..!

### 기타 섹션

- Import Table: 프로그램이 사용하는 외부 라이브러리(DLL)에 대한 정보를 담고 있으며, 해당 DLL의 주소를 참조하는 역할을 합니다.
- Export Table: 다른 프로그램에서 사용할 수 있는 함수나 변수의 주소를 정의합니다.

## 실제 .exe파일이 만들어지는 과정

- [Compiler](https://github.com/fkdl0048/CodeReview/blob/main/Language/C%2B%2B/Compiler/README.md)
- [Assembler](https://github.com/fkdl0048/CodeReview/blob/main/Language/C%2B%2B/Assembler/README.md)

실제 cpp 기준으로 정리한 내용이라 해당 내용을 참고하면 된다.

## exe 파일 실행 과정

앞 내용들을 대략적으로 이해했다면, exe 파일이 실행되는 과정을 따라가보자..!

*여기 부분이 중요.. 이해가 안된다면 위에서 부터 다시 이해하자!*

- **사용자 요청**
  - 사용자가 실행 파일(.exe)을 더블 클릭하거나 명령 프롬프트에서 실행 명령을 입력합니다.
- **파일 시스템 접근**
  - 운영 체제는 파일 시스템에서 해당 .exe 파일을 찾고 접근 권한을 확인합니다.
  - *운영체제 역할*
- **보안 검사**
  - 사용자 계정 컨트롤(UAC): 프로그램이 관리자 권한을 필요로 할 경우, UAC 창이 나타나 사용자에게 권한 승인을 요청합니다.
  - *흔히 보이는 그 관리자 권한 요청*
- **안티바이러스 검사**
  - 일부 보안 소프트웨어는 실행 전에 파일을 스캔하여 악성 코드 여부를 확인합니다.
- **프로세스 생성 요청**
  - 운영 체제의 커널은 새로운 프로세스를 생성하기 위해 내부적으로 시스템 호출을 수행합니다.
  - *System Call*
- **실행 파일 로딩**
  - PE(Portable Executable) 형식 분석: .exe 파일의 헤더 정보를 읽어 실행에 필요한 정보(코드, 데이터, 리소스 등)를 파악합니다.
  - 메모리 할당: 프로세스에 필요한 가상 메모리 공간을 할당합니다.
  - 코드 및 데이터 로드: 실행에 필요한 코드 섹션과 데이터 섹션을 메모리에 로드합니다.
- **의존성 해결 및 DLL 로딩**
  - 프로그램이 사용하는 동적 링크 라이브러리(DLL)를 찾고 로드합니다.
  - 필요한 경우, 추가적인 의존성 파일들도 로드됩니다.
- **프로세스 환경 설정**
  - 환경 변수, 작업 디렉토리, 표준 입출력 핸들 등이 설정됩니다.
  - 스레드 스택과 힙 영역이 초기화됩니다.
- **프로그램 초기화**
  - 런타임 라이브러리 초기화: C/C++의 경우, 런타임 라이브러리가 초기화되어 전역 변수와 static 변수가 설정됩니다.
  - 프로그램 진입점 호출: 일반적으로 main() 또는 WinMain() 함수가 호출됩니다.
- **코드 실행**
  - 프로그램의 메인 로직이 실행되며, 사용자의 입력이나 시스템 이벤트를 처리합니다.
- **프로세스 종료**
  - 프로그램이 종료되면, exit() 함수나 반환값을 통해 운영 체제에 종료 상태를 전달합니다.
  - 운영 체제는 할당된 메모리를 해제하고, 열린 파일이나 리소스를 정리합니다
- **후처리 작업**
  - 종료 코드가 부모 프로세스나 호출자에게 반환됩니다.
  - 로그 작성이나 이벤트 트리거 등의 후처리 작업이 수행될 수 있습니다.
- **추가 사항**
  - 멀티스레딩: 프로그램이 여러 개의 스레드를 생성하는 경우, 각 스레드는 독립적으로 스케줄링되고 관리됩니다.
  - 예외 처리: 실행 중 예외 상황이 발생하면, 예외 처리 루틴이 실행되어 오류를 처리하거나 프로그램을 안전하게 종료합니다.
  - 가상 메모리 및 페이지 매핑: 운영 체제는 가상 메모리 시스템을 통해 프로세스의 메모리 접근을 관리합니다.

## 참조

- https://play-with.tistory.com/323
- https://t0pli.tistory.com/100