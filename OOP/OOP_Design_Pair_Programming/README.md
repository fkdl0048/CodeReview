# 도메인 분석/설계 페어 프로그래밍

오브젝트, 객체지향의 사실과 오해를 읽고 도메인을 설계해보는 시간을 가지고자 세션을 개설

- [<오브젝트> 도메인 분석(페어 프로그래밍), 2024-05-30 [22:00]](https://github.com/BRIDGE-DEV/BRIDGE_BookClub/issues/129)
- 참여자: 정안, 동희, 우연

## 정안 도메인

### 버스 요금 계산 도메인

기본 요금, 거리 대비 요금, 시간대 요금(야간 할증), 나이대별 할인(어린이, 노인)

버스를 탈 때 NFC태깅이 가능한 카드인지 확인하고, 가능하다면 요금을 조회하여 잔액이 남아 있다면 기본 요금을 결제한다. 만약 잔액이 없다면 잔액부족을 띄운다.

결제된 기본 요금에서 내릴 때, 할증/할인을 계산하여 요금을 부과한다. (내릴 때도 마찬가지로 NFC, 요금조회를 거친다.)

할증/할인 조건은 다음과 같다.

- 공통
  - 할인과 할증의 조건과 규약은 본사의 요청에 따라 추가되거나 삭제될 수 있다.
- 할증
  - 모든 할증은 할인전에 적용되어야 한다.
  - 거리 대비 요금
    - 사용자가 탑승한 역을 기준으로 10정거장 이상 이동했다면 1정거장 당 100원의 추가 요금을 부과한다.
  - 시간대 요금
    - 탑승 시간을 기준으로먼 채크하며, 시간이 오전 01시 이후라면 할증 요금에 25%을 추가로 부과한다.
  - **(추가적인 과제)** 카드가 하차시 태크되지 않거나, 탑승한지 3시간 이상이 경과했다면 할증과 할인을 무시하고 10000원을 부과한다.
  - *추가 가능성..*
- 할인
  - 나이대 별 할인
    - 탑승자의 카드 정보에 할인조건인 나이대 할인이 들어가 있다면 해당 할인정보를 토대로 할인을 진행한다.
      - 어린이 90% 할인, 노인 80%할인
  - 국가 유공자 할인
    - 마찬가지로 할인 조건에 유공자 조건이 있다면 500원을 할인 해준다.

## 우연 도메인

### 만화카페 요금제 도메인

당신은 만화카페를 개업한 초보 사장님입니다. 수익을 극대화하고 고객을 유치하기 위해 요금제를 설계해봅시다.

#### **기본 요금 및 추가 요금**

- **기본 요금**
  - 기준 시간: 2시간
  - 음료 포함 (마진이 높은 음료와 세트로 구성)
- **추가 요금**
  - 두 가지 조건에 따라 부과
        1. **고객 유형**
            - **청소년**: 사용 가능한 금액이 적고, 장기적으로 성인 고객으로 전환될 가능성이 크므로 성인보다 요금을 낮게 설정
            - **성인:** 경제적 여유가 있어 청소년보다 높은 요금을 지불할 수 있음
        2. **방문 시간대**
            - **평일**: 방문 고객이 적으므로 요금을 저렴하게 책정
            - **주말 및 공휴일**: 추가 요금을 부과하여 수익 증대

#### **구체적인 요금 설정**

- **기본 요금**
  - 성인 기본 요금 (2시간, 음료 포함): 9,000원
  - 청소년 기본 요금 (2시간, 음료 포함): 8,000원
- **평일 추가 요금**
  - 성인 추가 요금: 10분당 500원
  - 청소년 추가 요금: 10분당 400원
- **주말 및 공휴일 추가 요금**
  - 평일 요금에 추가 요금 부과:
    - 성인: 10분당 600원 (100원 추가)
    - 청소년: 10분당 500원 (100원 추가)

#### **음료 요금**

- 판매 음료: 아메리카노, 아이스티, 카페라떼, 초코라떼
- 카페라떼와 초코라떼는 우유가 포함되므로, 이 두 메뉴 선택 시 500원의 추가 요금 부과

#### **할인 조건 (중복 가능)**

- **회원가입 유도**: 추가 금액의 10% 할인
- **후기 작성 유도**: 1시간 무료 제공
- **작은 요금 할인**: 총 사용시간에서 10분 미만의 시간은 계산에서 제외

#### 추가 조건 (선택)

- 1월 1일은 월요일
- 24시간 운영

### 도메인 분석 및 설계

기징 먼저 도메인의 흐름을 생각하면 다음과 같다.

손님 - 만화카페 - 요금 - 추가 요금 - 할인 요금

- 손님은 만화카페에 방문하여, 나갈 때, 요금을 지불한다.
- 만화카페는 손님의 요금과 추가요금, 할인 요금을 계산하여 반환한다.
- 요금은 기본 요금과 추가 요금으로 나뉜다.
- 기본 요금은 2시간을 기준으로 하며, 음료가 포함되어 있다.
  - 음료는 카페라떼와 초코라떼를 선택할 경우 추가 요금이 발생한다.
- 추가 요금은 고객 유형과 방문 시간대에 따라 다르다.
  - 고객 유형은 성인과 청소년으로 나뉜다.
  - 방문 시간대는 평일과 주말 및 공휴일로 나뉜다.

*디테일하게 들어가면 다음과 같다.*

- 손님
  - 만화카페 이용시간과 음료 선택을 한다.
  - 손님은 방문 시간대와, 고객 유형을 가지고 있다.(ex) 성인, 평일_10시)
- 만화카페
  - 손님의 요금을 계산한다.
  - 기본적으로 요금을 계산하는 요금 계산기를 가지고 있다.
    - `FeeCalculator`
      - `int CalculateFee(Customer customer)`
    - 요금 계산기는 기본 요금과 추가 요금을 계산한다.
    - 기본 요금은 생성자를 통해서 받고, 추가 요금은 인터페이스를 DI로 받는다.
      - 추가 요금은 고객 유형과 방문 시간대에 따라 다르다.
      - 추가 요금 인터페이스는 다음과 같다.
        - `IAdditionFee`
          - `int CalculateFee(Customer customer)`
          - 이를 상속받아 클래스로 나타내면 다음과 같다.
            - `WeekdayAdditionFee`
            - `WeekendAdditionFee`
            - `AdultAdditionFee`
            - `TeenagerAdditionFee`
  - 손님에게서 받은 음료에 따라 추가 요금을 부과한다.
    - 음료는 다음과 같다.
      - `Americano`
      - `IcedTea`
      - `CafeLatte`
      - `ChocoLatte`
    - 음료는 인터페이스로 나타내면 다음과 같다.
      - `IDrink`
        - `int CalculateFee()`
        - 생성자를 통해서 해당 음료의 추가 요금을 받는다.
        - 이를 나갈 때, 손님에게 반환한다.
  - 마찬가지로 할인 조건도 할인 계산기를 가지고 있다.
    - `DiscountCalculator`
      - `int CalculateDiscount(Customer customer)`
    - 할인 계산기는 할인 조건에 따라 할인을 계산한다.
    - 인터페이스는 `IDiscount`
      - 이를 상속받아 클래스로 나타내면 다음과 같다.
        - `MembershipDiscount`
        - `ReviewDiscount`
        - `SmallFeeDiscount`

```cs
// 손님 타입 Enum
enum CustomerType
{
    Adult,
    Teenager,
    // 추가 가능성..
}

// 손님
class Customer
{
    private CustomerType customerType;
    private LocalDateTime visitTime;
    private IDrink drink;

    public Customer(CustomerType customerType, LocalDateTime visitTime, IDrink drink)
    {
        this.customerType = customerType;
        this.visitTime = visitTime;
        this.drink = drink;
    }
}

// 음료
interface IDrink
{
    int CalculateFee();
}

class CafeLatte : IDrink
{
  private int additionalFee;

  public CafeLatte(int additionalFee)
  {
      this.additionalFee = additionalFee;
  }

    public int CalculateFee()
    {
        // 카페라떼 추가 요금 계산
    }
}

// 나머지 음료도 같은 방식으로 구현
...
//

// AdditionFee
interface IAdditionFee
{
    int CalculateFee(Customer customer);
}

class WeekdayAdditionFee : IAdditionFee
{
    private Map<CustomerType, int> additionalFee;

    public WeekdayAdditionFee(Map<CustomerType, int> additionalFee)
    {
        this.additionalFee = additionalFee;
    }

    public int CalculateFee(Customer customer)
    {
        // 평일 추가 요금 계산
    }
}

class WeekendAdditionFee : IAdditionFee
{
    private Map<CustomerType, int> additionalFee;

    public WeekendAdditionFee(Map<CustomerType, int> additionalFee)
    {
        this.additionalFee = additionalFee;
    }

    public int CalculateFee(Customer customer)
    {
        // 주말 추가 요금 계산
    }
}

// 나머지도 같은 방식으로 구현

// FeeCalculator
class FeeCalculator
{
    private Map<CustomerType, int> basicFee;
    private Map<CustomerType, IAdditionFee> additionFee;

    public FeeCalculator(Map<CustomerType, int> basicFee, Map<CustomerType, IAdditionFee> additionFee)
    {
        this.basicFee = basicFee;
        this.additionFee = additionFee;
    }

    public int CalculateFee(Customer customer, LocalDateTime leaveTime)
    {
        // 손님 정보를 조회하여 요금 계산
        // 성인, 청소년 구분 후 -> 방문 시간과 날짜를 통해 IAdditionFee에서 조회하여 맞는 정책으로 계산
        // 기본 요금 계산
        // 추가 요금 계산
    }
}

// IDiscount
interface IDiscount
{
    int CalculateDiscount(Customer customer);
}

class MembershipDiscount : IDiscount
{
    public MembershipDiscount(// 여기서 할인 조건에 대한 값을 받아야 하는데 너무 범위가 넓음,)
    {
        // 잘못 설계한 것 같음.. 
        // 다시 한다면 그냥 정적으로 할인 계산을 하는 것이 맞을 것 같다.
    }

    public int CalculateDiscount(Customer customer)
    {
        // 회원가입 유도 할인 계산
    }
}

// DiscountCalculator
class DiscountCalculator
{
    private List<IDiscount> discounts;

    public DiscountCalculator(List<IDiscount> discounts)
    {
        this.discounts = discounts;
    }

    public int CalculateDiscount(Customer customer)
    {
        // 손님 정보를 조회하여 할인 계산
        // 할인 조건에 따라 할인 계산
    }
}

// 만화카페
class ComicCafe
{
    private FeeCalculator feeCalculator;
    private DiscountCalculator discountCalculator;

    public ComicCafe(FeeCalculator feeCalculator, DiscountCalculator discountCalculator)
    {
        this.feeCalculator = feeCalculator;
        this.discountCalculator = discountCalculator;
    }

    public void CalculateFee(Customer customer, LocalDateTime leaveTime, IDrink drink)
    {
        // 요금 계산
        // 추가 요금 계산
        // 할인 계산
    }
}

// Main
public static void main(String[] args)
{
    // 요금 계산기 생성
    Map<CustomerType, int> basicFee = new HashMap<>();
    basicFee.Put(CustomerType.Adult, 9000);
    basicFee.Put(CustomerType.Teenager, 8000);

    Map<CustomerType, IAdditionFee> additionFee = new HashMap<>();
    additionFee.Put(CustomerType.Adult, new WeekdayAdditionFee(// 여기에 map 넣어야 함));
    additionFee.Put(CustomerType.Teenager, new WeekdayAdditionFee(// 여기에 map 넣어야 함));

    FeeCalculator feeCalculator = new FeeCalculator(basicFee, additionFee);

    // 할인 계산기 생성
    List<IDiscount> discounts = new ArrayList<>();
    discounts.Add(new MembershipDiscount());
    discounts.Add(new ReviewDiscount());
    discounts.Add(new SmallFeeDiscount());

    DiscountCalculator discountCalculator = new DiscountCalculator(discounts);

    // 만화카페 생성
    ComicCafe comicCafe = new ComicCafe(feeCalculator, discountCalculator);

    // 손님 생성
    Customer customer = new Customer(CustomerType.Adult, LocalDateTime.now());

    // 요금 계산
    comicCafe.CalculateFee(customer, LocalDateTime.now(), new CafeLatte(500));
}
```

## 동희 도메인

### 쇼핑몰 도메인

목표: 고객이 고른 제품들의 가격을 계산해 주문 가격을 내놓는다.

넘겨지는 정보: 제품들 정보, 주문하는 일시, 사용하는 쿠폰

- 할인
  - 제품 정보에 할인 정보들이 붙어 있다. (할인이 없는 제품도 존재)
  - 할인은 비율 할인으로만 존재하고, 제품 각각의 가격에 영향을 미친다.
  - 일반 할인
    - 원래 가격의 *할인 비율*로만 계산
  - 스페셜 타임 할인
    - 주문일시가 매일 PM 6~7시일 경우만 정해진 *할인 비율*로만 계산
- 쿠폰
  - 할인이 적용된 값에 적용한다. 쿠폰은 전체 가격에 영향을 미친다.
  - 쿠폰 적용 순서는 쿠폰의 정보가 넘겨지는 순서대로 적용한다.
  - 적용 방법
    - 비율형 쿠폰
      - 총 가격에서 정해진 비율을 할인
    - 금액형 쿠폰
      - 정해진 금액만을 마이너스해 할인
  - 적용 조건
    - 요일별 쿠폰
      - 구매하는 요일과 쿠폰의 적용요일이 같으면 쿠폰을 적용
    - 제품별 쿠폰
      - 주문 제품들 중 쿠폰의 적용 제품이 존재하면 쿠폰을 적용

### 도메인 분석 및 설계

가장 먼저 흐름에 대한 협력을 설계해보면, 다음과 같다.

고객 - 쇼핑몰 - 제품 - 할인 - 쿠폰

- 고객은 쇼핑몰에 주문을 한다.
- 쇼핑몰은 주문을 받아 제품들의 가격을 계산한다.
- 제품은 가격을 가지고 있고, 할인을 받을 수 있다.
- 할인은 제품의 가격을 바꾸는 역할을 한다.
- 쿠폰은 총 가격을 바꾸는 역할을 한다.
- 쇼핑몰은 쿠폰을 받아 총 가격을 계산한다.
- 총 가격을 고객에게 알려준다.

각 역할에 대한 책임을 나누어 본다.

- 고객
  - 주문을 한다.
  - 쇼핑몰에게 주문이라는 메시지를 전송한다.
    - 전송할 때, 고객이 주문한 제품들(문자열로 전달), 주문한 일시, 사용할 쿠폰을 같이 전송한다.
  - 즉, 쿠폰을 가지고 있어야 한다.
    - *코드로는 가지고 있지 않고 DI로 넣어줘도 되지만, 확장성을 고려해야 고객이 가지고 있는 것으로 한다.*
- 쇼핑몰
  - 주문을 받아 제품들의 가격을 계산한다.
  - 내부에 Product에 대한 정보를 가지고 있어야 함
    - 판매할 Product에 대한 정보를 Map으로 관리.
  - 총 가격을 계산한다.
  - 총 가격을 고객에게 알려준다.
- 제품
  - 가격과 할인 정보(합성), 제품의 이름을 가지고 있다.
  - 할인정보는 2가지로 나뉜다.
    - 일반 할인
    - 스페셜 타임 할인
    - 인터페이스로 나타내면 다음과 같다.
      - `Discountable`
        - `calculateDiscount(int price)`
      - 이를 상속받아 클래스로 나타내면 다음과 같다.
        - `NormalDiscount`
        - `SpecialTimeDiscount`
      - 이를 Product 클래스에 합성으로 추가한다.
- 쿠폰
  - 총 가격을 바꾸는 역할을 한다.
  - 쿠폰에 필요한 데이터
    - 쿠폰 이름이자 타입 (인터페이스를 상속)
      - 나머지 데이터는 내부 데이터로 가지고 있다.
  - 쿠폰은 적용 방법과 적용 조건으로 나뉜다.
    - 적용 조건
      - 요일별 쿠폰
      - 제품별 쿠폰
      - 적용 조건은 인터페이스로 나타내면 다음과 같다.
        - `CouponCondition`
          - `isSatisfied()`
        - 이를 상속받아 클래스로 나타내면 다음과 같다.
          - `DayCouponCondition`
            - 구매하는 요일과 쿠폰의 적용 요일이 같으면 쿠폰 적용
            - 따라서 추가적인 데이터로 요일을 가지고 있어야 한다.
              - `DayOfWeek`
          - `ProductCouponCondition`
            - 주문 제품들 중 쿠폰의 적용 제품이 존재하면 쿠폰 적용
            - 따라서 추가적인 데이터로 제품을 가지고 있어야 한다.
              - `Product`
    - 적용 방법 (못함..) 근데 아마 쿠폰에 인터페이스로 넣어줘서 쿠폰이 두개의 인터페이스를 상속받아 구현하게 할 것 같다.
      - 비율형 쿠폰
        - 총 가격에서 정해진 비율을 할인
      - 금액형 쿠폰
        - 정해진 금액만을 마이너스해 할인

```cs
// 고객
class Customer 
{
    private List<Coupon> coupons;
    private Money money;

    public Customer(List<Coupon> coupons, Money money) 
    {
        this.coupons = coupons;
        this.money = money;
    }

    public void Order(ShoppingMall shoppingMall, List<Product> products, LocalDateTime orderTime) 
    {
        shoppingMall.CalculatePrice(products, orderTime, coupons);
        // 금액 계산
    }
}

// 쇼핑몰
class ShoppingMall 
{
    private Map<String, Product> products;

    public ShoppingMall(Map<String, Product> products) 
    {
        this.products = products;
    }

    public void CalculatePrice(List<Product> products, LocalDateTime orderTime, List<Coupon> coupons) 
    {
        // 제품 가격 계산
        // 총 가격 계산
        // 쿠폰 적용
    }
}

// 제품
class Product 
{
    private String name;
    private int price;
    private Discountable discount;

    public Product(String name, int price, Discountable discount) 
    {
        this.name = name;
        this.price = price;
        this.discount = discount;
    }
}

// 할인
interface Discountable 
{
    int CalculateDiscount(int price);
}

class NormalDiscount : Discountable 
{
    public int CalculateDiscount(int price) 
    {
        // 일반 할인 계산
    }
}

class SpecialTimeDiscount : Discountable 
{
    public int CalculateDiscount(int price) 
    {
        // 스페셜 타임 할인 계산
    }
}

// 쿠폰
class Coupon 
{
    private String name;
    private CouponCondition condition;

    public Coupon(String name, CouponCondition condition) 
    {
        this.name = name;
        this.condition = condition;
    }
}

interface CouponCondition 
{
    boolean IsSatisfied();
}

class DayCouponCondition : CouponCondition 
{
    private DayOfWeek dayOfWeek;

    public DayCouponCondition(DayOfWeek dayOfWeek) 
    {
        this.dayOfWeek = dayOfWeek;
    }

    public boolean IsSatisfied() 
    {
        // 요일별 쿠폰 적용
    }
}

class ProductCouponCondition : CouponCondition 
{
    private Product product;

    public ProductCouponCondition(Product product) 
    {
        this.product = product;
    }

    public boolean IsSatisfied() 
    {
        // 제품별 쿠폰 적용
    }
}

// Main
public static void main(String[] args) 
{
    // 제품 정보
    Map<String, Product> products = new HashMap<>();
    products.Put("product1", new Product("product1", 10000, new NormalDiscount()));
    products.Put("product2", new Product("product2", 20000, new SpecialTimeDiscount()));

    // 쇼핑몰 생성
    ShoppingMall shoppingMall = new ShoppingMall(products);

    // 고객 생성
    List<Coupon> coupons = new ArrayList<>();
    coupons.Add(new Coupon("coupon1", new DayCouponCondition(DayOfWeek.MONDAY)));
    coupons.Add(new Coupon("coupon2", new ProductCouponCondition(products.Get("product1"))));
    Customer customer = new Customer(coupons, new Money(100000));

    // 주문
    customer.order(shoppingMall, (products("product1"), products("product2")), LocalDateTime.now());
}
```
