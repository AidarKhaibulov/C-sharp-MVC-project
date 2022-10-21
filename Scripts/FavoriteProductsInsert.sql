INSERT INTO "Test".public."Cart"("UserId", "Id")
VALUES (ToReplace, DEFAULT)
    ON CONFLICT ("UserId")do nothing