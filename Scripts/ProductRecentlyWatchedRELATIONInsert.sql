INSERT INTO "ProductRecentlyWatchedRelation" ("ProductId", "RecentlyWatchedCartId")
VALUES (FirstReplace, SecondReplace)
    ON CONFLICT ("ProductId","RecentlyWatchedCartId") do nothing