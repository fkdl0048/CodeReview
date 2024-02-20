#include <vector>
#include <iostream>

int main( )
{
   using namespace std;
   vector <int> v1;

   v1.push_back( 1 );
   cout << "The length of storage allocated is "
        << v1.capacity( ) << "." << endl;

   v1.push_back( 2 );
   cout << "The length of storage allocated is now "
        << v1.capacity( ) << "." << endl;

    v1.push_back( 3 );
    cout << "The length of storage allocated is now "
         << v1.capacity( ) << "." << endl;

    v1.push_back( 4 );
    cout << "The length of storage allocated is now "
         << v1.capacity( ) << "." << endl;
}