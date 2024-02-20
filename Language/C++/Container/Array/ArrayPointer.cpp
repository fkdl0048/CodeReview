#include <array>
#include <iostream>

typedef std::array<int, 4> Myarray;
int main()
{
    Myarray c0 = { 0, 1, 2, 3 };

    // display contents " 0 1 2 3"
    for (const auto& it : c0)
    {
        std::cout << " " << it;
    }
    std::cout << std::endl;

    // display first element " 0"
    Myarray::pointer ptr = c0.begin();
    std::cout << " " << *ptr;
    std::cout << std::endl;

    auto s = c0.begin();
    //int a = s[1];
    std::cout << " " << *s;

    return (0);
}