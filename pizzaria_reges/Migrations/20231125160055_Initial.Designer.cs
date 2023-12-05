﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pizzaria_reges.Data;

#nullable disable

namespace pizzaria_reges.Migrations
{
    [DbContext(typeof(IESContext))]
    [Migration("20231125160055_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("pizzaria_reges.Models.CarrinhoCompra", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("fk_PedidoID")
                        .HasColumnType("bigint");

                    b.Property<long?>("fk_ProdutoID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("fk_PedidoID");

                    b.HasIndex("fk_ProdutoID");

                    b.ToTable("CarrinhoCompra");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Cliente", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.Property<int>("cpf")
                        .HasColumnType("int");

                    b.Property<string>("data_nasc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("fk_EnderecoID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("fk_EnderecoID")
                        .IsUnique()
                        .HasFilter("[fk_EnderecoID] IS NOT NULL");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Endereco_cli", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<int>("Cep")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco_cli");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Pedido", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PedidoData")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<float?>("ValorTotal")
                        .HasColumnType("real");

                    b.Property<long?>("fk_ClienteID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("fk_ClienteID");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Produto", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Preco")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("pizzaria_reges.Models.CarrinhoCompra", b =>
                {
                    b.HasOne("pizzaria_reges.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("fk_PedidoID");

                    b.HasOne("pizzaria_reges.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("fk_ProdutoID");

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Cliente", b =>
                {
                    b.HasOne("pizzaria_reges.Models.Endereco_cli", "Endereco")
                        .WithOne("Cliente")
                        .HasForeignKey("pizzaria_reges.Models.Cliente", "fk_EnderecoID");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Pedido", b =>
                {
                    b.HasOne("pizzaria_reges.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("fk_ClienteID");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("pizzaria_reges.Models.Endereco_cli", b =>
                {
                    b.Navigation("Cliente");
                });
#pragma warning restore 612, 618
        }
    }
}
