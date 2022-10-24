INSERT INTO "Test".public."RecentlyWatchedCart"("UserId", "Id")
VALUES (ToReplace, DEFAULT)
    ON CONFLICT ("UserId")do nothing