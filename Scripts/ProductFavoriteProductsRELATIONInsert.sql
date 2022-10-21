INSERT INTO "ProductFavoriteProductsRELATION" ("ProductId", "FavoriteProductsId")
VALUES (FirstReplace, SecondReplace)
    ON CONFLICT ("ProductId","FavoriteProductsId") do nothing