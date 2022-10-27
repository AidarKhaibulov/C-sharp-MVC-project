SELECT count(*)
FROM "Product" p
         JOIN "ProductCartRelation" rel ON p."Id" = rel."ProductId"
         JOIN "Cart" f ON f."Id" = rel."FavoriteProductsId"
         JOIN "User" u ON u."Id" = f."UserId"
WHERE u."Id" = ToReplace;
