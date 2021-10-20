# 프로젝트 소개

사용자들이 모바일 환경에서 간편하게 게임을 제작하고, 만든 게임을 공유할 수 있는 플랫폼을 제공하는 프로젝트 입니다.

프로그래밍 지식이 없어도 제공되는 UI와 장르별 최적화된 컴포넌트를 통해 게임 설계가 가능합니다.

# 요구 환경

본 프로젝트를 실행하기 위해서는 Unity 2021.1.14f1 이상을 이용해야 합니다.

# 주요 기능

1. 비 전문가도 사용할 수 있는 블록형 코딩도구 제공
    - 별도의 프로그래밍 지식이 없어도 자신이 생각한 로직을 구현해 자신만의 게임을 만들 수 있습니다.
2. 게임 제작에 필요한 기본적인 에셋 제공
    - 캐릭터/오브젝트 모델링 등 게임 디자인에 필요한 컴포넌트들을 지원합니다.
        - 따라서 사용자들이 게임을 개발하는 경험에 더 집중할 수 있습니다.
3. 튜토리얼 제공
    - 각종 기능들을 더 쉽게 이해할 수 있도록, 게임 시나리오별 튜토리얼을 지원합니다.
4. 플레이 경험 공유
    - 자신이 만든 게임을 다른 사람들과 공유하여, 다른 사람들이 플레이 할 수 있도록 합니다.
    - 다른 유저들이 여러 게임을 통해 영감을 얻을 수 있도록 합니다.
    - 잘 만들어진 게임들을 추천할 수 있도록 추천 시스템을 제공합니다.

# 프로젝트 구성 (현재)

## 맵 에디터
**게임의 배경이 되는 맵을 제작할 수 있는 도구**

### 기능
1. 타일을 선택하여 원하는 위치에 설치하고 삭제 가능
2. 타일의 속성에 따라 지형이나 배경으로 활용 가능
3. 모바일 환경에서 편리한 작업을 위해, 이동 모드에서 드래그 터치를 통해 시점 이동이 가능

## 오브젝트 에디터
**상호작용 가능한 다양한 오브젝트 들을 만드는 도구**

### 기능
1. 다양한 종류의 오브젝트들 (캐릭터, NPC, 트리거, 장애물, 몹 등)을 제작
2. 사용자가 원하는 이미지를 선택하고, 자르고, 사이즈를 지정
3. 이동 모드에서 오브젝트를 드래그 할 경우, 원하는 위치로 오브젝트를 이동
    - Snap 여부에 따라 격자 고정 여부 결정

## 이벤트 에디터
**오브젝트들을 서로 상호작용 시키는 도구**

### 기능
1. 오브젝트에 대응되는 블록들이 자동으로 배치
2. 필요한 블록들을 추가적으로 배치 가능
3. 입력 단자와 출력 단자를 서로 연결하여, 사용자가 원하는 동작을 구성 가능
4. 원하는 위치에 카메라를 위치시키고 범위를 조절하여 게임을 만드는 유저가 의도하는 대로 화면이 보이도록 설정 가능
5. 화면상의 터치 입력 위치를 감지하고 출력하는 블록을 이용해 원하는 오브젝트가 화면 터치에 따라 움직일 수 있도록 구현
6. 오브젝트 속성 블록을 오브젝트 블록에 연결하여 오브젝트마다 다양한 특성(무게, 탄성계수 등등)을 설정 가능

## 게임 플레이어
**앞의 3가지 에디터를 사용해 만든 게임을 실행시킬 수 있는 기능** 