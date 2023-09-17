﻿// <auto-generated />
using System;
using FoodStuffs.Model.Data.EntityFramework;
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
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryRecipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryRecipe", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Blob", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Blob", (string)null);
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Directions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsForMealPlanning")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("char(128)")
                        .IsFixedLength();

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
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
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Blob", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Image", "Image")
                        .WithOne("Blob")
                        .HasForeignKey("FoodStuffs.Model.Data.Models.Blob", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Image", b =>
                {
                    b.HasOne("FoodStuffs.Model.Data.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.MealSet", b =>
                {
                    b.OwnsMany("FoodStuffs.Model.Data.Models.PantryIngredient", "PantryIngredients", b1 =>
                        {
                            b1.Property<int>("MealSetId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

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

                    b.OwnsMany("FoodStuffs.Model.Data.Models.Ingredient", "Ingredients", b1 =>
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

                            b1.Property<DateTime>("CreatedOn")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("IsCategory")
                                .HasColumnType("bit");

                            b1.Property<string>("ModifiedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("ModifiedOn")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("RecipeId", "Id");

                            b1.ToTable("Ingredient", (string)null);

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

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Image", b =>
                {
                    b.Navigation("Blob");
                });

            modelBuilder.Entity("FoodStuffs.Model.Data.Models.Recipe", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
