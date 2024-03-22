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
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("CategoryRecipe");
                });

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

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealSet", b =>
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

                    b.ToTable("MealSet", (string)null);
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

            modelBuilder.Entity("MealSetRecipe", b =>
                {
                    b.Property<int>("MealSetsId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesId")
                        .HasColumnType("int");

                    b.HasKey("MealSetsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("MealSetRecipe");
                });

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Image", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FoodStuffs.Model.Data.Models.ImageBlob", "ImageBlob", b1 =>
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

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealSet", b =>
                {
                    b.OwnsMany("FoodStuffs.Model.Data.Models.MealSetPantryIngredient", "PantryIngredients", b1 =>
                        {
                            b1.Property<int>("MealSetId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Quantity")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("MealSetId", "Id");

                            b1.ToTable("MealSet");

                            b1.ToJson("PantryIngredients");

                            b1.WithOwner()
                                .HasForeignKey("MealSetId");
                        });

                    b.Navigation("PantryIngredients");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Image", "PinnedImage")
                        .WithMany()
                        .HasForeignKey("PinnedImageId");

                    b.OwnsMany("FoodStuffs.Model.Data.Models.RecipeIngredient", "Ingredients", b1 =>
                        {
                            b1.Property<int>("RecipeId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTimeOffset>("CreatedOn")
                                .HasColumnType("datetimeoffset");

                            b1.Property<bool>("IsCategory")
                                .HasColumnType("bit");

                            b1.Property<string>("ModifiedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTimeOffset>("ModifiedOn")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<decimal>("Quantity")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("RecipeId", "Id");

                            b1.ToTable("RecipeIngredient", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RecipeId");
                        });

                    b.Navigation("Ingredients");

                    b.Navigation("PinnedImage");
                });

            modelBuilder.Entity("MealSetRecipe", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.MealSet", null)
                        .WithMany()
                        .HasForeignKey("MealSetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
