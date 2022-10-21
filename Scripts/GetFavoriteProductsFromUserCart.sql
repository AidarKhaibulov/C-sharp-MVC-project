SELECT p."Name"
  FROM "Product" p
    JOIN "ProductFavoriteProductsRELATION" rel ON p."FavoriteId" = rel."ProductId"
    JOIN "FavoriteProducts" f ON f."Id" = rel."FavoriteProductsId"
    JOIN "User" u ON u."Id" = f."UserId"
  WHERE u."Id" = ToReplace;
