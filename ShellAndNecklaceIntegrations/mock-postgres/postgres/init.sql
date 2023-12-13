create table filetype (
id serial primary key,
fileExtension varchar(6) not null
);
create table picture (
id serial primary key,
filetypeid integer,
imagename varchar(100),
foreign key (filetypeid) references filetype(id)
);
create table "status" (
id serial primary key,
"status" varchar(16)
);
create table item (
id serial primary key,
itemname varchar(32) not null,
"description" varchar(512) not null,
pictureid integer,
pricebase money,
statusid integer,
foreign key (pictureid) references picture(id),
foreign key (statusid) references "status"(id)
);
create table account (
id serial primary key,
"user_name" varchar(20),
email varchar(48) not null,
phone varchar(15),
"address" varchar(128),
verified boolean,
closed boolean
);
create table purchaseorder (
id serial primary key,
accountid integer,
notes varchar(256),
orderdate timestamp not null,
foreign key (accountid) references account(id) on update cascade
);
create table orderitem (
id serial primary key,
itemid integer,
orderid integer,
quantity integer not null,
pricepaid money not null,
foreign key (itemid) references item(id),
foreign key (orderid) references purchaseorder(id)
);
create table review (
id serial primary key,
rating integer not null check(rating between 0 and 10),
accountid integer,
reviewbody varchar(256) not null,
reviewdate timestamp not null,
foreign key (accountid) references account(id)
);
create table itemreview (
id serial primary key,
itemid integer,
accountid integer,
rating integer not null check (rating between 0 and 10),
reviewdate timestamp not null,
reviewtext varchar (1024) not null,
foreign key (itemid) references item(id),
foreign key (accountid) references account(id)
);
create table cart
(id serial primary key,
quantity int,
actualprice money,
itemid int,
accountid int,
foreign key (itemid) references item(id),
foreign key (accountid) references account(id)
);
insert into filetype(fileExtension)
values ('.bin'),
('.txt'),
('.png'),
('.jpeg'),
('.jpg');
insert into "status" ("status")
values ('AVAILABLE'),
('OUTOFSTOCK'),
('DISCONTINUED'),
('PREVIEW');
Insert into picture (filetypeid, imagename)
values (5, 'singleWinchesterGold'),
(5, 'doubleWinchesterBlue'),
(5, 'doubleRemingtonBlue'),
(5, 'singleWinchesterSilver'),
(5, 'doubleWinchesterPlain'),
(5, 'singleRemingtonBlue'),
(5, 'customSign1'),
(5, 'woodCutouts1'),
(5, 'turquoiseHoops');
insert into item (itemname, "description", pictureid, pricebase, statusid)
values ('Single Winchester Gold','Single 12g Winchester gold',1,15,1),
('Double Winchester Blue','Double 12g & 20g Winchester Blue',2,20,1),
('Double Remington Blue','Double 12g & 20g Remington Blue',3,20,1),
('Single Winchester silver','Single 12g Winchester silver',4,15,1),
('Double Winchester plain','Double 12g & 20g Winchester plain',5,20,1),
('Single Remington Blue','12g Remington single silver blue',6,15,1),
('Silver/Turquoise Hoops','Silver/turquoise hoops',7,20,1),
('Custom Sign Small','Custom wood signs',8,20,1),
('Custom Sign Medium','Custom wood signs',9,35,1),
('Custom Sign Large','Custom wood signs',10,50,1),
('Wood Cutout Small','Wood cutouts',11,8,1),
('Wood Cutout Medium','Wood cutouts',12,15,1),
('Wood Cutout Large','Wood cutouts',13,20,1);
Insert into account (email, phone, "address", verified, closed, "user_name")
values(1,'username@gmail.com',8018675309,'3035 s 400 e Ephraim UT, 84627','1','0','user');
Insert into account (email, phone, "address", verified, closed, "user_name")
values(1,'fluffiness@gmail.com',4358675309,null,'0','1','stranger');
Insert into account (email, phone, "address", verified, closed, "user_name")
values(1,'frickinusersman@gmail.com',null,'2819 n 200 e Ephram UT, 84627','0','0','engineer');
insert into review(rating, accountid, reviewbody, reviewdate) values(1,1,'Lorem ipsum dolor sit
amet, consectetur adipiscing elit, sed do eiusmod.','2022-10-19 10:23:54');
insert into review(rating, accountid, reviewbody, reviewdate) values(2,2,'Lorem ipsum dolor sit
amet, consectetur adipiscing elit, sed do eiusmod.','2022-10-19 10:23:54');
insert into review(rating, accountid, reviewbody, reviewdate) values(3,3,'Lorem ipsum dolor sit
amet, consectetur adipiscing elit, sed do eiusmod.','2023-10-19 10:23:54');
insert into review(rating, accountid, reviewbody, reviewdate) values(4,1,'Lorem ipsum dolor sit
amet, consectetur adipiscing elit, sed do eiusmod.','2023-11-19 10:23:54');
insert into review(rating, accountid, reviewbody, reviewdate) values(5,2,'Lorem ipsum dolor sit
amet, consectetur adipiscing elit, sed do eiusmod.','2023-12-19 10:23:54');
insert into itemreview(itemid,accountid, rating, reviewdate, reviewtext) values(7,1,1,'2022-10-19
10:23:54','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.');
insert into itemreview(itemid,accountid, rating, reviewdate, reviewtext) values(8,2,2,'2023-10-19
10:23:54','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.');
insert into itemreview(itemid,accountid, rating, reviewdate, reviewtext) values(9,3,3,'2023-10-19
10:23:54','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod.');
insert into itemreview(itemid,accountid, rating, reviewdate, reviewtext)
values(10,1,4,'2018-10-19 10:23:54','Lorem ipsum dolor sit amet, consectetur adipiscing elit,
sed do eiusmod.');
insert into itemreview(itemid,accountid, rating, reviewdate, reviewtext)
values(11,2,5,'2017-10-19 10:23:54','Lorem ipsum dolor sit amet, consectetur adipiscing elit,
sed do eiusmod.');
insert into purchaseorder(accountid, notes, orderdate) values(1,null,'2022-10-19 10:23:54');
insert into purchaseorder(accountid, notes, orderdate) values(2,'weird guy','2022-10-19
10:23:54');
insert into purchaseorder(accountid, notes, orderdate) values(3,'notes notes notes','2022-10-19
10:23:54');
insert into purchaseorder(accountid, notes, orderdate) values(1,'stuff','2022-10-19 10:23:54');
insert into purchaseorder(accountid, notes, orderdate) values(2,'weird person','2022-10-19
10:23:54');
insert into orderitem(itemid, orderid, quantity, pricepaid) values(7,1,1,15);
insert into orderitem(itemid, orderid, quantity, pricepaid) values(8,2,2,40);
insert into orderitem(itemid, orderid, quantity, pricepaid) values(9,3,3,50);
insert into orderitem(itemid, orderid, quantity, pricepaid) values(10,1,4,55);
insert into orderitem(itemid, orderid, quantity, pricepaid) values(11,2,5,100);