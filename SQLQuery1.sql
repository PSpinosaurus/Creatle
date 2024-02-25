use CreatleDb;

insert into dbo.Games(Name, Description, Colour) values('Overwatch','A first person shooter game.', 123); 


insert into dbo.Categories(Name) values('Gender');

insert into dbo.CategoriesValues(CategoryId, Value) values(1, 'Male');
insert into dbo.CategoriesValues(CategoryId, Value) values(1, 'Female');
insert into dbo.CategoriesValues(CategoryId, Value) values(1, 'Other');


insert into dbo.Categories(Name) values('Role');

insert into dbo.CategoriesValues(CategoryId, Value) values(2, 'Tank');
insert into dbo.CategoriesValues(CategoryId, Value) values(2, 'Damage');
insert into dbo.CategoriesValues(CategoryId, Value) values(2, 'Support');


insert into dbo.Categories(Name) values('Age');

insert into dbo.CategoriesValues(CategoryId, Value) values(3, '21');
insert into dbo.CategoriesValues(CategoryId, Value) values(3, '16');
insert into dbo.CategoriesValues(CategoryId, Value) values(3, '41');


insert into dbo.Categories(Name) values('Release date');

insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2014');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2015');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2016');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2017');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2018');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2019');
insert into dbo.CategoriesValues(CategoryId, Value) values(4, '2020');


insert into dbo.HeroMetadata(Name, Avatar) values('Ashe', 'https://oyster.ignimgs.com/mediawiki/apis.ign.com/overwatch-2/3/34/Ashe_Icon.png?width=325');
insert into dbo.HeroMetadata(Name, Avatar) values('Wrecking ball', 'https://oyster.ignimgs.com/mediawiki/apis.ign.com/overwatch-2/b/bc/Wrecking_Ball_Icon.png?width=325');
insert into dbo.HeroMetadata(Name, Avatar) values('D.Va', 'https://i.pinimg.com/736x/93/e2/8b/93e28bf94e71e344c481aebab9754219.jpg');


insert into dbo.HeroProfiles(GameId, CategoryId, HeroId, ValueId) values(1,1,1,2);
insert into dbo.HeroProfiles(GameId, CategoryId, HeroId, ValueId) values(1,1,2,1);
insert into dbo.HeroProfiles(GameId, CategoryId, HeroId, ValueId) values(1,1,3,2);

insert into dbo.Answers(Date, GameId, CategoryId, CategoryValueId) values('02/12/2024',1,1,1);
insert into dbo.Answers(Date, GameId, CategoryId, CategoryValueId) values('02/11/2024',1,1,2);

select * from dbo.Categories;
select * from dbo.CategoriesValues;
select * from dbo.Answers;
select * from dbo.Games;
select * from dbo.HeroMetadata;
select * from dbo.HeroProfiles;

