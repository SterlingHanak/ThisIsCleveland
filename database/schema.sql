-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

BEGIN TRAN;

-- CREATE statements go here
CREATE TABLE landmark (
	id integer IDENTITY NOT NULL,
	name varchar(100) NOT NULL,
	address varchar(100) NULL,
	phone_number varchar(12) NOT NULL,
	[description] varchar(MAX) NOT NULL,
	year_founded integer NULL,
	average_rating integer NOT NULL,
	relative_cost integer NOT NULL,
	annual_num_visitors integer NULL,
	website_url varchar(MAX) NOT NULL,
	latitude decimal(11, 8) NOT NULL,
	longitude decimal(11, 8) NOT NULL
	CONSTRAINT pk_landmark_id PRIMARY KEY(id),
	CONSTRAINT unique_landmark_name UNIQUE(name)
);

CREATE TABLE [day] (
	id integer IDENTITY NOT NULL,
	name varchar(10)
	CONSTRAINT pk_day_id PRIMARY KEY(id),
	CONSTRAINT unique_day_name UNIQUE(name)
);

CREATE TABLE daily_hours (
	id integer IDENTITY NOT NULL,
	day_id integer NOT NULL,
	landmark_id integer NOT NULL,
	time_open DateTime NOT NULL,
	time_closed DateTime NOT NULL,
	CONSTRAINT pk_daily_hours_id PRIMARY KEY(id),
	CONSTRAINT fk_daily_hours_day_id FOREIGN KEY(day_id) REFERENCES day(id),
	CONSTRAINT fk_daily_hours_landmark_id FOREIGN KEY(landmark_id) REFERENCES landmark(id)
);

CREATE TABLE restaurant (
	id integer IDENTITY NOT NULL,
	landmark_id integer NOT NULL,
	formality_level varchar(50) NOT NULL,
	cuisine_type varchar(50) NOT NULL,
	CONSTRAINT pk_restaurant_id PRIMARY KEY(id),
	CONSTRAINT fk_restaurant_landmark_id FOREIGN KEY(landmark_id) REFERENCES landmark(id) 
);

CREATE TABLE park (
	id integer IDENTITY NOT NULL,
	landmark_id integer NOT NULL,
	is_pet_friendly bit NOT NULL DEFAULT 0,
	has_picnic_area bit NOT NULL DEFAULT 0,
	has_restrooms bit NOT NULL DEFAULT 0,
	has_water_fountains bit NOT NULL DEFAULT 0,
	area_in_sqfeet decimal NOT NULL,
	CONSTRAINT pk_park_id PRIMARY KEY(id),
	CONSTRAINT fk_park_landmark_id FOREIGN KEY (landmark_id) REFERENCES landmark(id)
);

CREATE TABLE activity(
	id	Integer	IDENTITY NOT NULL,
	name varchar(100) NOT NULL,	
	CONSTRAINT pk_activity_id PRIMARY KEY (id)
);

CREATE TABLE park_activity (
	id integer IDENTITY NOT NULL,
	park_id integer NOT NULL,
	activity varchar(MAX) NOT NULL,
	CONSTRAINT pk_park_activity_id PRIMARY KEY(id),
	CONSTRAINT fk_park_activity_park_id FOREIGN KEY(park_id) REFERENCES park(id)
);

CREATE TABLE college (
	id integer IDENTITY NOT NULL,
	landmark_id integer NOT NULL,
	athletic_division varchar(50) NOT NULL,
	is_public bit NOT NULL DEFAULT 0,
	CONSTRAINT pk_college_id PRIMARY KEY(id),
	CONSTRAINT fk_college_landmark_id FOREIGN KEY(landmark_id) REFERENCES landmark(id)
);

CREATE TABLE category (
	id integer IDENTITY NOT NULL,
	name varchar(50) NOT NULL,
	CONSTRAINT pk_category_id PRIMARY KEY(id),
	CONSTRAINT unique_category_name UNIQUE(name)
);

CREATE TABLE landmark_category (
	landmark_id integer NOT NULL,
	category_id integer NOT NULL,
	CONSTRAINT pk_landmark_category PRIMARY KEY(landmark_id, category_id),
	CONSTRAINT fk_landmark_category_category_id FOREIGN KEY(category_id) REFERENCES category(id),
	CONSTRAINT fk_landmark_category_landmark_id FOREIGN KEY(landmark_id) REFERENCES landmark(id)
);

CREATE TABLE city_tours_user (
	id integer IDENTITY NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	username varchar(50) NOT NULL,
	[password] varchar(MAX) NOT NULL,
	salt varchar(MAX) NOT NULL,
	account_creation_date DateTime NOT NULL DEFAULT(GetDate())
	CONSTRAINT pk_user_id PRIMARY KEY(id),
	CONSTRAINT unique_user_username UNIQUE(username)
);

CREATE TABLE trip (
	id integer IDENTITY NOT NULL,
	city_tours_user_id integer NOT NULL,
	name varchar(500) NOT NULL,
	[description] varchar(MAX) NULL,
	trip_date DateTime NULL
	CONSTRAINT pk_trip_id PRIMARY KEY(id),
	CONSTRAINT fk_trip_city_tours_user_id FOREIGN KEY(city_tours_user_id) REFERENCES city_tours_user(id)
);

CREATE TABLE trip_landmark (
	trip_id integer NOT NULL,
	landmark_id integer NOT NULL,
	visit_order integer NOT NULL,
	CONSTRAINT pk_trip_landmark_trip_id_landmark_id PRIMARY KEY(trip_id, landmark_id),
	CONSTRAINT fk_trip_landmark_trip_id	FOREIGN KEY(trip_id) REFERENCES trip(id),
	CONSTRAINT fk_trip_landmark_landmark_id	FOREIGN KEY(landmark_id) REFERENCES landmark(id)
);

COMMIT;