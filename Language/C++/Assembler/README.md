# C++ Assembly

- [작성 이슈](https://github.com/fkdl0048/CodeReview/issues/58)

C++ 어셈블리어에 대해 정리합니다.

앞서 다룬 [C++ 컴파일 과정](https://github.com/fkdl0048/CodeReview/issues/56)에 이어서 전처리 다음 단계인 컴파일 단계에서 생성되는 어셈블리 명령어들이 어떻게 변환되는지 알아본다. 추가적으로 어셈블리 명령어나 CPU와 대응되는 명령어와 구조에 대해서도 정리한다.

## 목차

- 어셈블리어란?
- 어셈블리어를 보는 방법
- 어셈블리어의 구조

## 어셈블리어란?

- [가장 잘 설명된 유튭영상](https://www.youtube.com/watch?v=4gwYkEK0gOk)

컴퓨터 CPU에 입력되는 명령을 단순화하도록 설계된 저수준 프로그래밍 언어로 기계어에 가깝지만 사람이 이해하기 쉽도록 만들어진 언어이다. 여기서 언어의 혼동이 오지 않도록 저수준, low레벨이란 컴퓨터의 하드웨어와 직접적으로 연관된 프로그래밍 언어를 의미한다. 어셈블리가 여기에 해당한다. *사람이 읽을 수 있도록 추상화*

> 기계어는 CPU가 이해하는 0과 1로 이루어진 언어이다. 즉 사람이 봐도 이해할 수 없는 언어를 말한다.

## 어셈블리어를 보는 방법

일단 직접 해보면서 이해하는 것이 중요하기 때문에 아주 친숙한 코드인 `Hello, world!`로 어셈블리와 실제 실행파일이 나오기까지의 흐름을 다시 살펴본다.

```cpp
#include <iostream>

int main()
{
    std::cout << "Hello, World!" << std::endl;
    return 0;
}
```

다음과 같은 코드가 있을 때 이를 .exe 파일로 만들기 위해서는 내부적으로 여러 단계를 거친다. 해당 과정의 위의 링크인 컴파일러에서 다룬 내용을 참고하면 된다.

```bash
g++ -S -o main.s main.cpp
```

이 명령어는 `main.cpp`를 어셈블리어로 변환하여 `main.s` 파일로 저장한다.

- g++: GNU 컴파일러 모음(GCC)의 C++ 컴파일러를 사용한다는 뜻으로 Clang을 사용해도 된다. 다만 컴파일러마다 차이가 존재
- -S: 소스 파일을 **어셈블리 파일로 변환하도록 하는 옵션**이다.
- -o: 출력 파일을 지정하는 옵션으로 `main.s`로 지정한다.

추가로 -S 뒤에 `-O0`을 붙이면 최적화를 하지 않는다. 각각 레벨에 맞게 0~3까지 최적화 레벨을 조정할 수 있다. 또한 대소문자도 구분해야 한다.

이때 출력되는 파일은 `.s`의 확장자를 가진 파일이 어셈블리 파일로 뒤 어셈블리 구조 파트에서 다루겠다.

```bash
g++ -c -o main.o main.s
```

이 명령어는 `main.s`를 오브젝트 파일로 변환하여 `main.o` 파일로 저장한다.

- -c: 컴파일만 하고 링크는 하지 않는다. 이 옵션으로 생성되는 파일이 목적파일, 오브젝트 파일이다.

```bash
g++ -o main main.o
```

이 명령어는 `main.o`를 실행 파일로 변환하여 `main` 파일로 저장한다. 실행파일이기 때문에 필요한 라이브러리와 의존성을 포함해 프로그램을 실행 가능한 상태로 만든다.

## 어셈블리 구조

위에서 `Hello World`코드의 어셈블리 파일의 총 길이는 117줄이다. 이는 컴파일러의 영향을 받기 때문에 제각기 다른 형태로 생성된다. 또한 CPU마다의 차이도 있으니 참고하자.

```asm
	.file	"Test.cpp"
	.text
	.section .rdata,"dr"
_ZStL19piecewise_construct:
	.space 1
.lcomm _ZStL8__ioinit,1,1
	.def	__main;	.scl	2;	.type	32;	.endef
.LC0:
	.ascii "Hello, World!\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
	.seh_proc	main
main:
.LFB1559:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$32, %rsp
	.seh_stackalloc	32
	.seh_endprologue
	call	__main
	leaq	.LC0(%rip), %rdx
	movq	.refptr._ZSt4cout(%rip), %rcx
	call	_ZStlsISt11char_traitsIcEERSt13basic_ostreamIcT_ES5_PKc
	movq	.refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_(%rip), %rdx
	movq	%rax, %rcx
	call	_ZNSolsEPFRSoS_E
	movl	$0, %eax
	addq	$32, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.def	__tcf_0;	.scl	3;	.type	32;	.endef
	.seh_proc	__tcf_0
__tcf_0:
.LFB2049:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$32, %rsp
	.seh_stackalloc	32
	.seh_endprologue
	leaq	_ZStL8__ioinit(%rip), %rcx
	call	_ZNSt8ios_base4InitD1Ev
	nop
	addq	$32, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.def	_Z41__static_initialization_and_destruction_0ii;	.scl	3;	.type	32;	.endef
	.seh_proc	_Z41__static_initialization_and_destruction_0ii
_Z41__static_initialization_and_destruction_0ii:
.LFB2048:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$32, %rsp
	.seh_stackalloc	32
	.seh_endprologue
	movl	%ecx, 16(%rbp)
	movl	%edx, 24(%rbp)
	cmpl	$1, 16(%rbp)
	jne	.L6
	cmpl	$65535, 24(%rbp)
	jne	.L6
	leaq	_ZStL8__ioinit(%rip), %rcx
	call	_ZNSt8ios_base4InitC1Ev
	leaq	__tcf_0(%rip), %rcx
	call	atexit
.L6:
	nop
	addq	$32, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.def	_GLOBAL__sub_I_main;	.scl	3;	.type	32;	.endef
	.seh_proc	_GLOBAL__sub_I_main
_GLOBAL__sub_I_main:
.LFB2050:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$32, %rsp
	.seh_stackalloc	32
	.seh_endprologue
	movl	$65535, %edx
	movl	$1, %ecx
	call	_Z41__static_initialization_and_destruction_0ii
	nop
	addq	$32, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.section	.ctors,"w"
	.align 8
	.quad	_GLOBAL__sub_I_main
	.ident	"GCC: (x86_64-win32-seh-rev0, Built by MinGW-W64 project) 8.1.0"
	.def	_ZStlsISt11char_traitsIcEERSt13basic_ostreamIcT_ES5_PKc;	.scl	2;	.type	32;	.endef
	.def	_ZNSolsEPFRSoS_E;	.scl	2;	.type	32;	.endef
	.def	_ZNSt8ios_base4InitD1Ev;	.scl	2;	.type	32;	.endef
	.def	_ZNSt8ios_base4InitC1Ev;	.scl	2;	.type	32;	.endef
	.def	atexit;	.scl	2;	.type	32;	.endef
	.section	.rdata$.refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_, "dr"
	.globl	.refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_
	.linkonce	discard
.refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_:
	.quad	_ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_
	.section	.rdata$.refptr._ZSt4cout, "dr"
	.globl	.refptr._ZSt4cout
	.linkonce	discard
.refptr._ZSt4cout:
	.quad	_ZSt4cout
```

천천히 하나씩 뜯어보면 다음과 같다.

### 파일 및 섹션 정의

```asm
    .file	"Test.cpp"
    .text
    .section .rdata,"dr"
```

어셈블리 파일의 첫 부분은 **메타데이터**와 **코드의 섹션**을 정의한다.

- `.file`: 파일의 이름을 나타낸다.
- `.text`: 코드 섹션 (.text 섹션은 실행할 명령어가 포함됨)
- `.section .rdata,"dr"`: 읽기 전용 데이터 섹션 (상수 데이터가 여기에 위치한다.)

### `_ZStL19piecewise_construct` 선언

```asm
    _ZStL19piecewise_construct:
        .space 1
```

`_ZStL19piecewise_construct`는 C++ 표준 라이브러리에서 정의된 특별한 객체인 `std::piecewise_construct`를 나타낸다. 탬플릿 클래스의 생성자에서 부분적으로 객체를 생성할 때 사용된다.

`.space 1`은 1바이트의 공간을 할당한다는 의미로 특별한 초기화를 필요로 하지 않는 객체이다. 실질적인 내용은 없다.

즉, C++ 표준 라이브러리의 전역 객체을 위한 공간을 예약하는 것이며, 큰 역할을 하지 않는다.

### `.lcomm _ZStL8__ioinit,1,1`

이는 C++ 표준 라이브러리에서 I/O 스트림인 `std::cin`, `std::cout`등의 초기화를 처리하는 내부 객체이다. `1,1`에서 첫 번째 1은 크기를 나타내며, 여기서는 1바이트를 할당한다. 두 번째 1은 메모리 정렬을 의미한다.

이 변수는 입출력 스트림의 초기화를 담당하는데, 전역적으로 실행 시 초기화된다.

### 글로벌 및 외부 심볼 선언

```asm
    .globl	main
    .def	main;	.scl	2;	.type	32;	.endef
```

- `.globl`: 글로벌 심볼을 선언하는 것으로 다른 파일에서도 참조할 수 있다.
- `.def`: 심볼을 정의하는 것으로 심볼의 속성을 정의한다.
  - `main`마찬가지로 `main` 함수를 정의한다.
  - `.scl 2`: 심볼의 범위를 나타내는데, 2는 전역 범위를 의미한다. *스코프 클래스*
  - `.type 32`: 심볼의 타입을 나타내는데, 32는 함수를 의미한다.
  - `.endef`: 심볼 정의를 끝내는 것으로 심볼의 속성을 정의하는 것이 끝났음을 나타낸다.

### `Hello, World!` 문자열 선언

```asm
    .LC0:
        .ascii "Hello, World!\0"
```

- `.LC0`: 문자열의 레이블을 나타낸다. 프로그램은 이 레이블을 참조하여 문자열을 출력한다.
- `.ascii`: C++ 코드에서 `"Hello, World!"`로 선언된 문자열을 정의하는 부분이다.

### `main` 함수

```asm
    main:
    .LFB1559:
        pushq	%rbp
        .seh_pushreg	%rbp
        movq	%rsp, %rbp
        .seh_setframe	%rbp, 0
        subq	$32, %rsp
        .seh_stackalloc	32
        .seh_endprologue
        call	__main
        leaq	.LC0(%rip), %rdx
        movq	.refptr._ZSt4cout(%rip), %rcx
        call	_ZStlsISt11char_traitsIcEERSt13basic_ostreamIcT_ES5_PKc
        movq	.refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_(%rip), %rdx
        movq	%rax, %rcx
        call	_ZNSolsEPFRSoS_E
        movl	$0, %eax
        addq	$32, %rsp
        popq	%rbp
        ret
        .seh_endproc
```

이 부분은 함수의 프로로그(prologue), 즉 함수의 시작 부분을 나타내는 어셈블리 코드다. 이 코드는 스택 프레임을 설정하고, 레지스터를 저장한다.

- `main:`: `main` 함수의 시작을 나타낸다.
- `.LFB1559`: LFB는 `ocal Function Block`의 약자로 이 함수 블록의 시작을 의미하는 레이블이다.
- `pushq %rbp`: 베이스 포인터를 스택에 저장하는 명령어로 main함수가 호출될 때, 현재 베이스 포인터 값을 스택에 저장한다. 함수가 끝났을 때는 이를 복구할 수 있도록 한다.
- 스택은 함수 호출 시마다 새로운 **스택 프레임**을 할당하여 함수 간의 호출 정보를 관리한다.
- `.seh_pushreg %rbp`: SEH는 Structured Exception Handling의 약자로 예외 처리를 위한 명령어이다. 예외처리를 위해 %rbp 레지스터를 스택에 저장하는 것을 예외 처리 정보에 기록한다. 예외가 발생하면 SEH시스템은 이 정보를 사용해 레지스터 %rbp를 복구한다.
- `movq %rsp, %rbp`: `movq`는 64비트 값을 복사하는 명령어이다. 이 명령어는 현재 스택 포인터 값을 베이스 포인터 레지스터에 저장한다. 즉, 현재 스택의 최상위 주소를 베이스 포인터로 설정하여 새로운 스택 프레임을 구성한다.
- `.seh_setframe %rbp, 0`: SEH 시스템에 현재 함수의 스택 프레임을 설정한다. 이 명령어는 현재 함수의 스택 프레임을 설정하고, SEH 시스템에 이 정보를 기록한다.
- `subq $32, %rsp`: `subq`는 64비트 정수값을 빼는 명령어로 `$32`만큼 스택 포인터(rsp)의 값을 감소 시킨다. 스택에 32바이트의 공간을 할당하는 것이다. 이 공간은 함수 내에서 사용될 지역 변수 또는 임시 데이터를 위한 공간을 제공한다. 스택은 위에서 아래로 증가하므로 스택 포인터를 감소시키면 스택의 최상위에 공간을 할당하는 것이다.
- `.seh_stackalloc 32`: SEH 시스템에 32바이트 스택 공간을 할당했음을 기록한다.
- `.seh_endprologue`: SEH 시스템에 함수의 프롤로그가 끝났음을 알린다. 프로로그는 함수가 시작될 때 스택을 설정하고 레지스터를 저장하는 과정으로, 이 지시문이 끝나면 본격적인 함수의 본문이 시작된다.
- `call __main`: `__main` 함수를 호출한다. 이 함수는 C++에서 전역 객체와 정적 객체의 초기화 작업을 처리하는 함수이다. 주로 C++ 런타임 초기화 작업을 수행한다. 즉, `main`함수가 실행되기 전에 전역 객체 및 정적 객체의 초기화가 완료되어야 하므로, 컴파일러는 이 함수를 호출하여 초기화를 보장한다.
- `leaq .LC0(%rip), %rdx`: `leaq`는 주소를 계산하는 명령어로 메모리 주소를 레지스터에 로드하는데 사용한다. `.LCO`는 "Hello, World!" 문자열이 저장된 위치를 나타내는 레이블이다. `%rip`는 현재 명령어의 주소를 나타내는 'Register Instruction Pointer'의 약자로, 현재 명령어의 주소를 나타낸다. rip로 부터 상대적인 오프셋을 계산하여 메모리 주소를 레지스터에 저장한다. 즉, "Hello, World!" 문자열의 주소를 `%rdx` 레지스터에 저장한다.
- `movq .refptr._ZSt4cout(%rip), %rcx`: `movq`는 64비트 값을 메모리에서 레지스터로 복사하는 명령어로 `.refptr._ZSt4cout(%rip)`는 `std::cout` 객체의 주소를 나타내는 레이블이다. `%rcx`레지스터에 `std::cout` 객체의 주소를 저장한다.
- `call _ZStlsISt11char_traitsIcEERSt13basic_ostreamIcT_ES5_PKc`: `call`은 함수 호출이기 때문에 이 함수는 `<<`을 사용하기 위한 함수이다.
- `movq .refptr._ZSt4endlIcSt11char_traitsIcEERSt13basic_ostreamIT_T0_ES6_(%rip), %rdx`: `std::endl` 객체의 주소를 `%rdx` 레지스터에 저장한다.
  - 네이밍이 길고 복잡한 이유는 C++의 컴파일러가 이름 맹글링을 통해 함수 이름을 변경하기 때문이다. 각각 의미가 있기에 해석도 가능하다.
- `movq %rax, %rcx`: `<<` 연산자를 사용하기 위해 `%rax` 레지스터의 값을 `%rcx` 레지스터로 복사한다.
  - 레지스터 값을 복사하는 이유는 호출 규약정도로 이해하면 된다.
- `call _ZNSolsEPFRSoS_E`: `<<` 연산자를 사용하기 위한 함수를 호출한다.
- `movl $0, %eax`: `movl`은 32비트 값을 복사하는 명령어, `$0`은 0을 의미하며 main의 반환값을 0으로 설정한다. 즉 정상적으로 종료되었음을 나타낸다.
- `addq $32, %rsp`: `addq`는 64비트 값을 더하는 명령어로 `$32`만큼 스택 포인터(rsp)의 값을 증가시킨다. 이는 함수의 종료로 인해 할당된 스택 프레임을 제거하는 것이다.
  - 값을 더하는데 이유는 스택은 위에서 아래로 증가하므로 스택 포인터를 증가시키면 더 낮은 주소를 가리키게 되어 스택 프레임을 제거하는 것이다. 반대로 스택에서 값을 제거할 때는 스택 포인터를 증가시켜 메모리를 해제한다. 자세한 내용은 아래에서 다시 설명
- `popq %rbp`: `popq`는 스택에서 값을 꺼내는 명령어로 `%rbp` 레지스터에 저장된 베이스 포인터를 스택에서 꺼내어 복원한다. 즉, 함수에서 빠져나와 이전 함수로 돌아갈 때 베이스 포인터를 복구하는 것이다.
- `ret`: `ret`은 함수 반환 명령어로 스택에서 복귀 주소를 꺼내어 **프로그램의 제어를 반환**한다. 즉, main이 종료되고 프로그램이 호출된 위치로 돌아간다.
- `.seh_endproc`: SEH 시스템에 함수의 종료를 알린다.
- `.def __tcf_0; .scl 3; .type 32; .endef`: `__tcf_0` 함수를 정의한다. 이 함수는 C++에서 전역 객체 및 정적 객체의 소멸자를 처리하는 함수이다. 프로그램이 종료될 때 전역 객체 및 정적 객체의 소멸 작업을 수행한다. `scl 3`은 심볼의 범위를 나타내는데, 3은 지역 범위를 의미한다. `type 32`는 함수를 나타내는데, 32는 함수를 의미한다. `endef`는 심볼 정의를 끝내는 것으로 심볼의 속성을 정의하는 것이 끝났음을 나타낸다.
- `.seh_proc __tcf_0`: SEH 시스템에 `__tcf_0` 함수의 시작을 알린다.

이 부분에 대한 내용은 스택프레임, 베이스 포인터에 해당되는 내용이 그림으로 그려져야 좋기에 해당 내용을 살펴보는 것을 추천한다.

- [스택 프레임에 관한 정리](https://github.com/fkdl0048/CodeReview/tree/main/Language/C%2B%2B/MemoryStructure#%EC%8A%A4%ED%83%9D-%ED%94%84%EB%A0%88%EC%9E%84%EC%9D%98-%EA%B5%AC%EC%A1%B0)

이 아래부터의 내용은 **C++ 프로그램에서 전역 객체 초기화 및 소멸 작업**에 해당하는 내용이다.

- .LFB2049 블록
  - 전역적으로 정의된 I/O 객체(예: std::cout)의 소멸자를 호출하는 코드
- _Z41__static_initialization_and_destruction_0ii
  - 전역 객체의 초기화 및 소멸을 처리하는 함수
- _GLOBAL__sub_I_main
  - 전역 객체의 초기화 작업
- .ctors 섹션
  - 전역 객체의 생성자(constructor)를 위한 공간
- .def 및 .rdata 섹션
  - .rdata 섹션은 읽기 전용 데이터(Read-only Data)를 저장하는 곳

## 정리

### 전체적인 흐름

모든 명령어와 모든 함수들을 외울 필요는 없다. 실제로 로우레벨의 동작 흐름만 보면 된다고 생각하기에 main함수의 시작과 끝을 보고 이해하는 것이 중요하다. 전역객체의 초기화나 생성은 필요할 때 추가로 정리할 예정

흐름은 스택 프레임의 형태를 실제로 레지스터를 활용해 메모리에 저장하고 불러오는 과정의 반복이다. 물론 중간중간 예외를 위해 SEH를 사용하고 있다. 베이스 포인터와 스택 포인터의 관계와 실제로 어떻게 제어권을 넘기는지를 위의 설명을 보고 이해하자.

축약된 용어가 많아서 이를 좀 더 자세하게 설명한다.

- `pushq`: push quadword의 약자로 64비트 값을 스택에 저장하는 명령어이다.
  - 이처럼 명령어뒤에 붙는 `q`는 quadword의 약자로 64비트를 의미한다. `d`는 doubleword로 32비트를 의미한다. `w`는 word로 16비트를 의미한다.
- `popq`: pop quadword의 약자로 64비트 값을 스택에서 꺼내는 명령어이다.
- `movq`: move quadword의 약자로 64비트 값을 복사하는 명령어이다.
- `subq`: substract quadword의 약자로 64비트 값을 빼는 명령어이다.
- `addq`: add quadword의 약자로 64비트 값을 더하는 명령어이다.
- `call`: 함수를 호출하는 명령어이다.
- `ret`: 함수를 종료하고 호출자로 돌아가는 명령어이다.
- `leaq`: load effective address quadword의 약자로 주소를 계산하는 명령어이다.

### 스택의 구조

좀 더 이해를 돕기 위해 다음 내용을 참조한다.

- [스택 프레임에 관한 정리](https://github.com/fkdl0048/CodeReview/tree/main/Language/C%2B%2B/MemoryStructure#%EC%8A%A4%ED%83%9D-%ED%94%84%EB%A0%88%EC%9E%84%EC%9D%98-%EA%B5%AC%EC%A1%B0)
- [메모리에 관하여](https://github.com/fkdl0048/CodeReview/blob/main/ComputerScience/Memory/README.md)

스택의 동작 방식은 위에서 아래로 자라난다. 즉, 새로운 값을 스택에 추가할 때마다 스택 포인터(%rsp)는 감소하여 더 낮은 주소를 가리킨다. 반대로, 스택에 저장된 값을 제거할 때는 스택 포인터를 증가시켜 스택에서 메모리를 해제하게 된다.

스택 공간 할당과 해제과정을 좀 더 자세히 정리하면 다음과 같다.

#### 스택프레임 관리

스택 공간 할당의 경우 함수가 호출되면, 해당 함수가 필요로 하는 로컬 변수나 임시 저장 공간을 위해 스택에서 공간을 할당한다. 이때, **스택 포인터(%rsp)**는 감소하여, 더 많은 스택 공간을 할당하게 된다.

```asm
	subq $32, %rsp
```

스택 공간 해제의 경우 함수가 종료될 때, 할당된 스택 공간을 해제해야 합니다. 이때 스택 포인터(%rsp)를 증가시켜 이전에 할당한 스택 공간을 다시 복구합니다.

```asm
	addq $32, %rsp
```

스택 프레임을 설정하는 과정은 함수가 호출되면 기본 **베이스 포인터(%rbp)**를 저장하고, 새로운 스택 프레임을 할당한다.

```asm
	pushq %rbp
	movq %rsp, %rbp
```

스택이 복구될 때(제어권 반환) 저장해두었던 베이스 포인터를 복구하고, 원래 호출된 함수의 상태로 돌아간다.

```asm
	popq %rbp
```

다음은 함수 호출 및 레지스터 관리에 대한 내용이다.

#### 함수 호출 및 레지스터 관리

어셈블리에서는 함수 호출 규약에 따라, 함수 인자와 반환값을 특정 레지스터에 저장하고 사용한다. 주요 레지스터는 다음과 같다.

- 인자 전달: 함수 호출 시 첫 번째 인자는 **%rcx**, 두 번째 인자는 **%rdx**에 저장
- 반환값 저장: 함수의 반환값은 **%rax** 레지스터에 저장

용어에 대한 설명을 추가.

- `%rax`
  - R은 Register의 약자로 레지스터를 의미한다. A는 Accumulator의 약자로 누산기를 의미한다. 즉, 누산기 레지스터를 의미한다. 연산 결과나 반환값을 저장하는 용도로 사용
- `%rcx`
  - Counter Register를 의미 전통적으로 루프 카운터로 사용되었지만, 함수 호출 시 첫 번째 인자를 저장하는 데 사용됩니다.
- `%rdx`
  - Data Register를 의미 전통적으로 데이터를 저장하는 레지스터로 사용되었지만, 함수 호출 시 두 번째 인자를 저장하는 데 사용됩니다.

```asm
movq %rax, %rcx   # 함수의 반환값을 %rcx로 복사하여 다음 함수 호출에 사용
call _ZStls...    # std::cout << "Hello, World!" 호출
```
