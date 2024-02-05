#include <vector>
#include <iostream>

int main()
{
    std::vector<int> v1;

    v1.push_back(10);
    v1.push_back(20);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;

    v1.insert(v1.begin() + 1, 15);

    std::cout << "The first element is " << v1[0] << std::endl;
    std::cout << "The second element is " << v1[1] << std::endl;
    std::cout << "The third element is " << v1[2] << std::endl;
}