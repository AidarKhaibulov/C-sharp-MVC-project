INSERT INTO "FavoriteProducts" ("UserId", "Id")
VALUES (ToReplace, DEFAULT)
    ON CONFLICT ("UserId")do nothing