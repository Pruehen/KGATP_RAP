# KGATP_RAP

[![Build Status](https://img.shields.io/travis/사용자명/프로젝트명.svg)](https://travis-ci.org/사용자명/프로젝트명)
[![Coverage Status](https://img.shields.io/coveralls/사용자명/프로젝트명.svg)](https://coveralls.io/github/사용자명/프로젝트명?branch=master)
[![License](https://img.shields.io/github/license/사용자명/프로젝트명.svg)](LICENSE)

## 소개
24년 하반기 기획플밍 미니합반 프로젝트의 리포지토리입니다.

## 시작하기
이 지침을 따라 프로젝트 사본을 로컬에서 설정하고 실행하십시오.

### 요구 사항
- 유니티 씬 변경, 스크립트 작성 등 내부 기능에 대한 변경은 DEV 브랜치에서 파생된 "각자의" 브랜치에 커밋 후 푸시할 것.
- 예시) 씬에 오브젝트를 배치했다. 작업은 DEV_KJH (KJH는 이름 약자) 브랜치에서 진행하며, 커밋 및 푸시도 해당 브랜치로 한다.
- 예시) 다른 사람의 작업물을 시험삼아 받아서 적용해봐야 하는 경우, 자신의 브랜치로 다른 사람의 브랜치를 병합하여 작업한다.
- 
- DEV 브랜치는 팀장 이외 변경 금지. 팀장이 각자의 작업물을 병합한 후 DEV에 직접 병합하게 됨. DEV 브랜치에서 실수로 작업한 경우, 작업물이 날아가도 책임지지 않음.
- 리소스 추가는 RES 브랜치에서 진행할 것. 추가한 리소스를 받아봐야 할 시, RES 브랜치를 각자의 브랜치로 병합할 것.
- Master 브랜치는 불변성을 유지할 것. (작업 금지)

### 설치
```Fork
# 클론 저장소

# 디렉토리 이동
cd 프로젝트명

# 종속성 설치
npm install
