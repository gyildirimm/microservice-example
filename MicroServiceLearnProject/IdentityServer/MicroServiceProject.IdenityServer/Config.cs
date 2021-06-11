﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace MicroServiceProject.IdenityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes = { "catalog_fullpermission" } },
            new ApiResource("resource_photo_stock"){Scopes = { "photo_stock_fullpermission" } },
            new ApiResource("resource_basket"){Scopes = { "basket_fullpermission" } },
            new ApiResource("resource_discount"){Scopes = { "discount_fullpermission" } },
            new ApiResource("resource_order"){Scopes = { "order_fullpermission" } },
            new ApiResource("resource_payment"){Scopes = { "payment_fullpermission" } },
            new ApiResource("resource_gateway"){Scopes = { "gateway_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName) 
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource() {Name = "roles", DisplayName = "Roles", Description = "Kullanıcı Rolleri", UserClaims = new []{ "role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission", "Photo Stock API için full erişim"),
                new ApiScope("basket_fullpermission", "Basket API için full erişim"),
                new ApiScope("discount_fullpermission", "Discount API için full erişim"),
                new ApiScope("order_fullpermission", "Order API için full erişim"),
                new ApiScope("payment_fullpermission", "Order API için full erişim"),
                new ApiScope("gateway_fullpermission", "Gateway için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                  new Client
                  {
                      ClientName = "Asp.Net Core MVC",
                      ClientId = "WebMvcClient",
                      ClientSecrets = {new Secret("secret".Sha256()) },
                      AllowedGrantTypes = GrantTypes.ClientCredentials,
                      AllowedScopes = { 
                          "catalog_fullpermission", 
                          "photo_stock_fullpermission",
                          "gateway_fullpermission",
                          IdentityServerConstants.LocalApi.ScopeName }
                  },
                  new Client
                  {
                      ClientName = "Asp.Net Core MVC",
                      ClientId = "WebMvcClientForUser",
                      AllowOfflineAccess = true,
                      ClientSecrets = {new Secret("secret".Sha256()) },
                      AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                      AllowedScopes = {
                          "basket_fullpermission",
                          "discount_fullpermission",
                          "order_fullpermission",
                          "payment_fullpermission",
                          "gateway_fullpermission",
                          IdentityServerConstants.StandardScopes.Email, 
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile,
                          IdentityServerConstants.StandardScopes.OfflineAccess,
                          IdentityServerConstants.LocalApi.ScopeName,
                          "roles"
                      },
                      AccessTokenLifetime = (int)(DateTime.Now.AddDays(7) - DateTime.Now).TotalSeconds,
                      RefreshTokenExpiration = TokenExpiration.Absolute,
                      AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                      RefreshTokenUsage = TokenUsage.ReUse
                  }
            };
    }
}