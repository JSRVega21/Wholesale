﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wholesale.Server.Data;

#nullable disable

namespace Wholesale.server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250306161606_Initial4")]
    partial class Initial4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wholesale.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("SlpCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlpName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("U_CodigoPOS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserRoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("UserWholesale");
                });

            modelBuilder.Entity("Wholesale.Models.VisitDetail", b =>
                {
                    b.Property<int>("VisitDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitDetailId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordinates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("SalespersonCode")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("SalespersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisitHeaderId")
                        .HasColumnType("int");

                    b.HasKey("VisitDetailId");

                    b.HasIndex("VisitHeaderId");

                    b.ToTable("VisitDetails");
                });

            modelBuilder.Entity("Wholesale.Models.VisitHeader", b =>
                {
                    b.Property<int>("VisitHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitHeaderId"));

                    b.Property<DateTime?>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalVisits")
                        .HasColumnType("int");

                    b.HasKey("VisitHeaderId");

                    b.ToTable("VisitHeaders");
                });

            modelBuilder.Entity("Wholesale.Models.User", b =>
                {
                    b.OwnsOne("Wholesale.Models.RecordLog", "RecordLog", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Usuario que creo el registro");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Fecha y hora de creación del registro");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit")
                                .HasComment("Registro activo");

                            b1.Property<bool>("IsSystem")
                                .HasColumnType("bit")
                                .HasComment("Es un registro del sistema, los registros del sistema no pueden ser eliminados");

                            b1.Property<string>("ObjectKey")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasComment("Código identificador del objeto representado en el registro");

                            b1.Property<string>("RecordKey")
                                .IsRequired()
                                .HasMaxLength(36)
                                .HasColumnType("nvarchar(36)")
                                .HasComment("Identificador único del registro, asignado en el momento de creación");

                            b1.Property<DateTime>("SyncDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha de sincronización");

                            b1.Property<int>("SyncStatus")
                                .HasColumnType("int")
                                .HasComment("Estatus de sincronización del registro");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Ultimo usuario que modificó el registro");

                            b1.Property<DateTime>("UpdatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha y hora de actualización del registro");

                            b1.HasKey("UserId");

                            b1.ToTable("UserWholesale");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RecordLog");
                });

            modelBuilder.Entity("Wholesale.Models.VisitDetail", b =>
                {
                    b.HasOne("Wholesale.Models.VisitHeader", "VisitHeader")
                        .WithMany("Details")
                        .HasForeignKey("VisitHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Wholesale.Models.RecordLog", "RecordLog", b1 =>
                        {
                            b1.Property<int>("VisitDetailId")
                                .HasColumnType("int");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Usuario que creo el registro");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Fecha y hora de creación del registro");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit")
                                .HasComment("Registro activo");

                            b1.Property<bool>("IsSystem")
                                .HasColumnType("bit")
                                .HasComment("Es un registro del sistema, los registros del sistema no pueden ser eliminados");

                            b1.Property<string>("ObjectKey")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasComment("Código identificador del objeto representado en el registro");

                            b1.Property<string>("RecordKey")
                                .IsRequired()
                                .HasMaxLength(36)
                                .HasColumnType("nvarchar(36)")
                                .HasComment("Identificador único del registro, asignado en el momento de creación");

                            b1.Property<DateTime>("SyncDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha de sincronización");

                            b1.Property<int>("SyncStatus")
                                .HasColumnType("int")
                                .HasComment("Estatus de sincronización del registro");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Ultimo usuario que modificó el registro");

                            b1.Property<DateTime>("UpdatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha y hora de actualización del registro");

                            b1.HasKey("VisitDetailId");

                            b1.ToTable("VisitDetails");

                            b1.WithOwner()
                                .HasForeignKey("VisitDetailId");
                        });

                    b.Navigation("RecordLog");

                    b.Navigation("VisitHeader");
                });

            modelBuilder.Entity("Wholesale.Models.VisitHeader", b =>
                {
                    b.OwnsOne("Wholesale.Models.RecordLog", "RecordLog", b1 =>
                        {
                            b1.Property<int>("VisitHeaderId")
                                .HasColumnType("int");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Usuario que creo el registro");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Fecha y hora de creación del registro");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit")
                                .HasComment("Registro activo");

                            b1.Property<bool>("IsSystem")
                                .HasColumnType("bit")
                                .HasComment("Es un registro del sistema, los registros del sistema no pueden ser eliminados");

                            b1.Property<string>("ObjectKey")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasComment("Código identificador del objeto representado en el registro");

                            b1.Property<string>("RecordKey")
                                .IsRequired()
                                .HasMaxLength(36)
                                .HasColumnType("nvarchar(36)")
                                .HasComment("Identificador único del registro, asignado en el momento de creación");

                            b1.Property<DateTime>("SyncDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha de sincronización");

                            b1.Property<int>("SyncStatus")
                                .HasColumnType("int")
                                .HasComment("Estatus de sincronización del registro");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasComment("Ultimo usuario que modificó el registro");

                            b1.Property<DateTime>("UpdatedDate")
                                .HasColumnType("datetime2")
                                .HasComment("Ultima fecha y hora de actualización del registro");

                            b1.HasKey("VisitHeaderId");

                            b1.ToTable("VisitHeaders");

                            b1.WithOwner()
                                .HasForeignKey("VisitHeaderId");
                        });

                    b.Navigation("RecordLog");
                });

            modelBuilder.Entity("Wholesale.Models.VisitHeader", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
