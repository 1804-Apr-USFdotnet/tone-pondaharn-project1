--insert into Restaurants.Restaurant([name], [address], city, [state], zipCode, phoneNumber) values ('Bluths Original Frozen Banana', '1 Boardwalk', 'Newport Beach', 'CA', '00002', '(555)555-2121');

--select * From Restaurants.Restaurant

insert into Restaurants.Reviews(ID, RestaurantId, reviewer, review, rating, Active) values(1, 'tone', 'authentic!!!', 10, 1)