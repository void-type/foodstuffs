﻿// <auto-generated />
using System;
using FoodStuffs.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodStuffs.Model.Migrations
{
    [DbContext(typeof(FoodStuffsContext))]
    partial class FoodStuffsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.HasIndex("RecipeId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MealPlan", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlanPantryShoppingItemRelation", b =>
                {
                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("MealPlanId", "ShoppingItemId");

                    b.HasIndex("ShoppingItemId");

                    b.ToTable("MealPlanPantryShoppingItemRelation", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlanRecipeRelation", b =>
                {
                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("MealPlanId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("MealPlanRecipeRelation", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Directions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsForMealPlanning")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PinnedImageId")
                        .HasColumnType("int");

                    b.Property<int?>("PrepTimeMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PinnedImageId");

                    b.ToTable("Recipe", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.RecipeShoppingItemRelation", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingItemId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "ShoppingItemId");

                    b.HasIndex("ShoppingItemId");

                    b.ToTable("RecipeShoppingItemRelation", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.ShoppingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingItem", (string)null);
                });

            modelBuilder.Entity("RecipeCategoryRelation", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeCategoryRelation", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Image", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FoodStuffs.Model.Data.Models.Image.ImageBlob#FoodStuffs.Model.Data.Models.ImageBlob", "ImageBlob", b1 =>
                        {
                            b1.Property<int>("ImageId")
                                .HasColumnType("int");

                            b1.Property<byte[]>("Bytes")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");

                            b1.HasKey("ImageId");

                            b1.ToTable("ImageBlob", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ImageId");
                        });

                    b.Navigation("ImageBlob")
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlanPantryShoppingItemRelation", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.MealPlan", null)
                        .WithMany("PantryShoppingItemRelations")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.ShoppingItem", "ShoppingItem")
                        .WithMany()
                        .HasForeignKey("ShoppingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingItem");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlanRecipeRelation", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.MealPlan", null)
                        .WithMany("RecipeRelations")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", "Recipe")
                        .WithMany("MealPlanRelations")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Image", "PinnedImage")
                        .WithMany()
                        .HasForeignKey("PinnedImageId");

                    b.OwnsMany("FoodStuffs.Model.Data.Models.Recipe.Ingredients#FoodStuffs.Model.Data.Models.RecipeIngredient", "Ingredients", b1 =>
                        {
                            b1.Property<int>("RecipeId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("IsCategory")
                                .HasColumnType("bit");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<decimal>("Quantity")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("RecipeId", "Id");

                            b1.ToTable("RecipeIngredient", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RecipeId");
                        });

                    b.Navigation("Ingredients");

                    b.Navigation("PinnedImage");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.RecipeShoppingItemRelation", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", null)
                        .WithMany("ShoppingItemRelations")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.ShoppingItem", "ShoppingItem")
                        .WithMany()
                        .HasForeignKey("ShoppingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingItem");
                });

            modelBuilder.Entity("RecipeCategoryRelation", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealPlan", b =>
                {
                    b.Navigation("PantryShoppingItemRelations");

                    b.Navigation("RecipeRelations");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("MealPlanRelations");

                    b.Navigation("ShoppingItemRelations");
                });
#pragma warning restore 612, 618
        }
    }
}
