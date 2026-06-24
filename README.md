# RogueJack

블랙잭에 로그라이트 요소를 결합한 싱글 플레이 덱빌딩 게임입니다.

![ex_pedia](https://github.com/seomu7/Unity_RogueJack/blob/main/SampleImage/ex_pedia.png)

## 기획 및 특징

* Slay the Spire, Balatro에서 영감을 받은 덱빌딩, 칩(유물) 시스템
* 라운드를 진행하고 칩을 획득해 추가 점수, 어드벤티지를 획득
* 짧은 1 라운드 소요 시간과 단순한 규칙으로 가볍고 빠른 진행
* 랜덤으로 제시되는 칩을 통해 매 판 차별화되는 경험 제공

![ex_server1](https://github.com/seomu7/Unity_RogueJack/blob/main/SampleImage/ex_server1.png)

## 기술 스택

* Unity
* C#
* Jetson Nano
* Linux
* FastAPI

![ex_2](https://github.com/seomu7/Unity_RogueJack/blob/main/SampleImage/ex_2.png)

## 게임 플레이 및 주요 시스템

* 플레이어는 매 라운드마다 딜러와 블랙잭을 플레이합니다.
* 라운드 승리시, 라운드 승리 점수를 획득합니다.

### 칩 시스템

![ex_1](https://github.com/seomu7/Unity_RogueJack/blob/main/SampleImage/ex_1.png)

* 라운드의 승패와 무관하게, 라운드 종료시 3개의 선택지로 제시되는 칩 중 하나를 선택할 수 있습니다.
* 칩은 점수 획득에 도움을 주거나, 시스템을 변경(ex. 버스트 한도를 22로 설정)하여 플레이어에게 어드벤티지를 제공합니다.
* 플레이어는 칩을 기반으로 더욱 다양한 전략을 선택할 수 있습니다.

### 랭킹 시스템

![ex_server2](https://github.com/seomu7/Unity_RogueJack/blob/main/SampleImage/ex_server2.png)

* 최종 라운드 종료시, 랭킹 시스템을 이용할 수 있습니다.
* \+ 아이콘을 클릭해 이름을 입력하여 랭킹을 등록하고, 현재 내 순위를 확인할 수 있습니다.
* 트로피 아이콘을 클릭해 상위 랭커 3명의 이름과 점수를 확인할 수 있습니다.

## 개발 과정 및 후기

### Unity

* 여러 칩을 설계하면서, 칩이 공통적으로 가져야하는 기능과 각 칩마다 다른 속성, 그리고 칩이 객체로서 가지는 정보를 구분해보며 클래스와 객체, Scriptable Object에 대해 자세히 익힐 수 있었습니다.
* 시스템이나 기능을 추가하면서 기존 코드를 수정하고 리팩토링하는 과정이 많았는데, 이를 수행하며 객체간 결합도를 낮게 설계하는 것이 유지보수 단계에서 중요한 이유를 실감할 수 있었습니다.
* DOTween, UnityEvnent를 통해 델리게이트와 람다식을 적극 활용해볼 수 있었으며, 향후 UniTask, LINQ 등의 기능들도 사용하여 코드를 리팩토링 해보고 싶습니다.

### Server

* FastAPI를 이용해 Jetson Nano에 직접 간단한 개인 서버와 DB를 구축하였습니다.
* SSH를 통해 원격으로 서버 작업을 하면서 리눅스 시스템과 설정을 익힐 수 있었습니다.
* 서버와 클라이언트를 연결하는 과정에서 DuckDNS, 공유기 포트포워딩을 사용하였습니다.
* 통신은 FastAPI와 UnityEngine.Networking을 사용하였으며, 향후 이 과정을 C 또는 C#을 이용해 직접 소켓을 다루고 프로토콜을 만들어 제어해보고 싶습니다.
