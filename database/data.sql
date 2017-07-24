-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN TRAN;

INSERT INTO landmark VALUES ('TownHall', '1909 W 25th St, Cleveland, OH 44113', '216.344.9400', 'Locally sourced & innovative bar bites & vegan options served in a contemporary setting with patio.', null, 4.3, 2, null, 'townhallohiocity.com');
INSERT INTO landmark VALUES ('Barrio', '503 Prospect Ave E, Cleveland, OH 44115', '216.862.4652', 'Local chain serving signature & build-your-own tacos, plus cocktails, in a hip, casual space.', null, 4.4, 1, null, 'barrio-tacos.com');
INSERT INTO landmark VALUES ('Tilted Kilt Pub and Eatery', '21 Prospect Ave E, Cleveland, OH 44115', '216.771.5458', 'UK-inspired pub chain with kilt-clad waitresses serving fish 'n' chips & bar food.', null, 4.2, 2, null, 'tiltedkilt.com');
INSERT INTO landmark VALUES ('Shooters On the Water', '1148 Main Ave, Cleveland, OH 44113', '216.861.6900', 'A high-volume venue offering a long menu of familiar fare & a patio showing a sound & light show.', null, 4.5, 2, null, 'shootersflats.com');
INSERT INTO landmark VALUES ('Hofbräuhaus', '1550 Chester Ave, Cleveland, OH 44115', '216.621.2337', 'German microbrewery with an on-site restaurant & beer garden offering traditional eats & live music.', null, 4.5, 3, null, 'hofbrauhauscleveland.com');
INSERT INTO landmark VALUES ('TOMO Sushi & Hibachi Restaurant', '1293 W 9th St, Cleveland, OH 44113', '216.696.4444', 'Huge 4-floor venue offering Japanese hibachi fare & sushi plus lounges with DJs, music & dancing.', null, 3.5, 3, null, 'tomohibachiandsushi.com');
INSERT INTO landmark VALUES ('Brasa Grill Brazilian Steakhouse', '1300 W 9th St, Cleveland, OH 44113', '216.575.0699', 'Lively Brazilian churrascaria with all-you-can-eat carved meats served by gauchos plus a full bar.', null, 3.5, 3, null, 'brasagrillsteakhouse.com');
INSERT INTO landmark VALUES ('Thirsty Parrot', '812 Huron Rd E, Cleveland, OH 44115', '216.685.3200', 'Relaxed cantina near Progressive Field, offering pub fare with pre- and post-game specials.', null, 3.5, 2, null, 'thirstyparrot.com');

COMMIT;