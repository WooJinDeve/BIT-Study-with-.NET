--insert into Product values('생수', 500,'생수입니다');

--update Product set price = 500, Description = '설명문을 수정합니다'
--where PNAME = '환타';

--delete from Product where pname = '생수';

--select PNAME, PRICE from Product where pid = 1000;

select Count(*) from Product;
select sum(price) from product;