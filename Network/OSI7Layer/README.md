# OSI 7 Layer

OSI(Open Systems Interconnection) 7계층 모델은 네트워크 통신을 여러 계층으로 나누어 설명하는 모델입니다. 각 계층은 독립적으로 작동하며, 각 계층이 담당하는 역할이 다릅니다. 이를 통해 네트워크 통신의 복잡성을 줄이고, 문제를 특정 계층에서 분리하여 해결할 수 있습니다.

## 1. 물리 계층 (Physical Layer)

- **역할**: 전기적, 물리적인 연결을 담당하는 계층. 데이터를 실제로 전송하기 위한 물리적 매체(케이블, 무선 신호)를 다룹니다.
- **주요 요소**: 케이블, 스위치, 허브, 네트워크 어댑터
- **데이터 단위**: 비트(Bit)

## 2. 데이터 링크 계층 (Data Link Layer)

- **역할**: 물리 계층에서 전송된 데이터를 프레임으로 포장하고, 오류 검출 및 수정, 흐름 제어를 담당합니다.
- **주요 요소**: 스위치, 브리지, MAC 주소
- **프로토콜**: 이더넷, Wi-Fi, PPP(Point-to-Point Protocol)
- **데이터 단위**: 프레임(Frame)

## 3. 네트워크 계층 (Network Layer)

- **역할**: 데이터를 목적지까지 라우팅하고, 다른 네트워크 간의 데이터 전달을 담당합니다. IP 주소를 사용하여 데이터를 전송합니다.
- **주요 요소**: 라우터, IP 주소
- **프로토콜**: IP(Internet Protocol), ICMP(Internet Control Message Protocol)
- **데이터 단위**: 패킷(Packet)

## 4. 전송 계층 (Transport Layer)

- **역할**: 종단 간(end-to-end) 데이터 전송을 관리하고, 데이터의 신뢰성 및 흐름 제어를 담당합니다. 오류 검출 및 수정 기능도 제공합니다.
- **주요 요소**: 포트 번호
- **프로토콜**: TCP(Transmission Control Protocol), UDP(User Datagram Protocol)
- **데이터 단위**: 세그먼트(Segment, TCP), 데이터그램(Datagram, UDP)

## 5. 세션 계층 (Session Layer)

- **역할**: 통신 세션을 관리하는 계층. 세션 생성, 유지, 종료를 담당하며, 응용 프로그램 간의 데이터 교환을 조율합니다.
- **주요 요소**: 세션 관리
- **프로토콜**: NetBIOS, RPC(Remote Procedure Call)
- **데이터 단위**: 데이터(Data)

## 6. 표현 계층 (Presentation Layer)

- **역할**: 데이터의 표현을 처리하는 계층. 데이터의 암호화, 압축, 인코딩/디코딩을 담당하며, 서로 다른 시스템 간 데이터 표현 형식을 맞춥니다.
- **주요 요소**: 암호화, 데이터 압축
- **프로토콜**: JPEG, GIF, SSL/TLS
- **데이터 단위**: 데이터(Data)

## 7. 응용 계층 (Application Layer)

- **역할**: 사용자와 네트워크 간의 상호작용을 처리하는 계층. 응용 프로그램이 네트워크 서비스에 접근할 수 있도록 인터페이스를 제공합니다.
- **주요 요소**: 웹 브라우저, 이메일 클라이언트
- **프로토콜**: HTTP, FTP, SMTP, DNS, POP3
- **데이터 단위**: 데이터(Data)