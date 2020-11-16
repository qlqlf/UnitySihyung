# UnitySihyung
배경사진 위에 UI를 구축하여 기능을 구현하였다. 배경사진과 Virus이미지, 케릭터 그림은 모두 직접 그린 그림을 사용하였고 sprite폴더에 저장되어있다.
Canvas안에 Canvas Group을 사용하여 각 UI메뉴들을 통제하였고 FallingVirus라는 화면에 떠다니는 UI image를 따로 구현하였다.
FallingVirus는 각 이미지에 virusScript를 설정하여 구현하였다.
각 메뉴들은 MenuType과 Btn Type 스크립트를 활용해 기능을 구현하였다. 한 메뉴에서 다른 메뉴로 이동할 때 현재 메뉴는 끄고 다음 메뉴를 띄우는 형식이다. 메뉴안의 버튼들의 기능은 Btn Type스크립트를 사용하였다. Btn Type애서 각 버튼의 기능에 맞는 메소드를 버튼과 연결하여 구현하였다. ToastMessage는 github자료를 사용하였다.
링크 : https://github.com/herbou/Unituts__toast-messages
StartMenu는 어플 실행 시 가장 먼저 나오는 UI로 Login과 Sign UP기능을 수행한다. 각 버튼을 클릭할 시 LoginMenu, SignUpMenu로 이동한다.
SignUpMenu는 InputField를 활용해 ID를 받아 DataBase에 접근하여 중복체크(ID Check)를 한 뒤 나머지(Email, PassWord)값을 받고 DB에 회원 정보를 저장한다.
LoginMenu는 InputField를 활용해 ID와 PassWord값을 받고 DB에 접근하여 회원정보를 확인 한 뒤 MainMenu로 이동한다. User가 회원 정보를 모른다면 Find ID/PW 버튼을 눌러 FindMenu로 이동하도록 구현하였다.
FindMenu는 InputField를 활용해 Email값을 받고 FindID 버튼을 누를 시 DB에 접근하여 회원 정보를 받은 뒤 ToastMessage로 회원의 ID를 보여준다. 이때 회원의 ID가 여러개라면 모두 보여준다. PassWord의 경우 ID와 Email값을 사용한다. 방법은 Find ID와 동일하다.
MainMenu는 InputField를 활용해 User의 nickname(변경가능한 이름)을 받고 Play 버튼 클릭 시 DB에 접근하여 값을 저장한 뒤 PlayMenu로 이동한다. Tutorial, Cash Shop 버튼은 각각 TutorialMenu, CashShopMenu로 이동하도록 구현하였다. Logout 버튼은 StartMenu로 이동한다.
PlayMenu는 User가 접속할 수 있는 방을 ScrollView를 활용해 10개 구현하였고 각 방의 참여자목록은 DB와 Socket을 사용하여 구현한다.(예정) 각 방의 Join 버튼을 클릭할 시 그 방의 JoinMenu로 이동한다. Back 버튼을 클릭할 시 PlayMenu로 이동한다.
JoinMenu는 DB에 저장된 방의 이름, 게임 시작 준비를 한 User의 수, 방에 접속한 각 유저의 Character와 NickName을 보여준다. Start 버튼을 클릭할 시 DB에 접근하여 StartCount를 1증가시키고 다시 누르면 1감소시키도록 구현하고 Socket을 활용하여 Start 버튼을 누른 User의 수를 실시간으로 보여주도록 구현할 예정이다. 모든 User(5인)들이 Start버튼을 누를 시 게임 Scene으로 전환하도록 구현할 예정이다. Back 버튼은 PlayMenu로 이동한다.
TutorialMenu는 게임의 설명, 조작법, 플레이방법 등을 설명하는 메뉴이다.(구현 예정)
CashShopMenu는 각 캐릭터(7개)의 이미지와 함께 Toggle을 보여준다. Image와 Toggle은 Grid Layout을 사용하여 정렬하였고 Toggle은 Toggle Group을 활용해 1가지만 선택이 되도록 하였다. Toggle을 선택하고 Select버튼을 누르면 DB에 접근하여 User의 케릭터 정보에 저장한다. Back 버튼을 누를 시 PlayMenu로 이동한다. User의 DB에 보유하고 있는 케릭터 정보를 저장해 두고 Buy버튼을 활용하여 케릭터를 구매하는 등 수익화를 할 수 있도록 구현할 예정이다.
