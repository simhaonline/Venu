﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Venu.Ticketing.Infrastructure;

namespace Venu.Ticketing.API.Infrastructure.Migrations
{
    [DbContext(typeof(TicketingContext))]
    partial class TicketingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.CustomerAggregate.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.EventAggregate.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("events");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.SeatingAggregate.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Column")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOccupied")
                        .HasColumnType("boolean");

                    b.Property<int>("Row")
                        .HasColumnType("integer");

                    b.Property<string>("SectionId")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("seats");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.TicketAggregate.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.VenueAggregate.Section", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Columns")
                        .HasColumnType("integer");

                    b.Property<int>("Ordinal")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.Property<string>("VenueId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("sections");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.VenueAggregate.Venue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EventId")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("venues");
                });

            modelBuilder.Entity("Venu.Ticketing.Domain.AggregatesModel.VenueAggregate.Section", b =>
                {
                    b.HasOne("Venu.Ticketing.Domain.AggregatesModel.VenueAggregate.Venue", null)
                        .WithMany("Sections")
                        .HasForeignKey("VenueId");
                });
#pragma warning restore 612, 618
        }
    }
}
