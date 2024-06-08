# Dangling Pointer

포인터 변수를 delete, free할 경우 메모리가 할당 해제되었다고 해도 변수가 가리키는 주소값은 사라지지 않는다. 따라서 그 포인터 변수를 다시 참조하려고 하면 에러가 발생한다. 이러한 포인터를 `Dangling Pointer`라고 한다.

