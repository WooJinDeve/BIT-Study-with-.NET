-- 좌석표 출력 (table, select)
select * from [Table];

-- 좌석 선택 입력 (Table, update)
update [Table] set State = 1 where TID = {0}; // Tid

-- 회원 전화번호 (member, select)
select MID from Member where PHONE = {0}; // phone

-- 입실 최종 입력
insert into PCData values({0},{1},getdate(),null,0); // mid, tid


-- 퇴실 
update [Table] set State = 0 where TID = 1;

-- 금액산정(10분당 500원)
update PCData set ETIME = GETDATE() where MID = {0} and price = 0; // mid
update PCData set PRICE = (DATEDIFF(MINUTE,STIME,ETIME)/10)*500 where mid = {0} and price = 0;  // mid

-- 고객수
select count(*) from PCData;

-- 전체 수익
select sum(PRICE) from PCData;

--좌석별 수익
select sum(PRICE), TID from PCData group by TID Having Sum(price) != 0;