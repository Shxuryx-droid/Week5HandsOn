# Week5HandsOn
Kafka Chat Application with C#

This repository contains a simple chat application built using C# Windows Forms, demonstrating integration with Apache Kafka as a streaming platform. It allows users to send and receive chat messages in real-time.
Table of Contents

    Introduction

    Kafka Concepts

    Prerequisites

    Installation and Setup

        Kafka and Zookeeper

        Creating Kafka Topic

    How to Run

        Kafka Console Producer

        Kafka Console Consumer

        C# Windows Forms Client

    Project Structure

    References

Introduction

This project showcases how to build a basic chat application using C# that leverages Apache Kafka for message streaming. The application consists of a Windows Forms client that can produce (send) messages to a Kafka topic and consume (receive) messages from the same topic, effectively acting as a real-time chat client.
Kafka Concepts

Before diving into the application, it's essential to understand some core Kafka concepts:

    Kafka: A distributed streaming platform capable of handling trillions of events a day. It's used for building real-time data pipelines and streaming applications.

    Kafka Architecture: Kafka operates as a cluster of one or more servers (brokers).

    Topics: A category or feed name to which records are published. Topics are always multi-subscriber; that is, a topic can have zero, one, or many consumers that subscribe to the data written to it.

    Partitions: Topics are divided into a set of partitions. Each partition is an ordered, immutable sequence of records that is continually appended to a structured commit log. Records in a partition are assigned a sequential ID number called the "offset" that uniquely identifies each record within the partition.

    Brokers: A Kafka server (node) is called a broker. Kafka brokers are stateless, so they use Zookeeper for maintaining their cluster state.

    Kafka plug in .NET: Libraries like Confluent.Kafka provide .NET clients to interact with Kafka brokers.

Prerequisites

Before you can run this application, you need to have the following installed:

    Apache Kafka and Apache Zookeeper: Download from the official Apache Kafka website.

    .NET SDK: .NET 5.0 or later (or the version compatible with your Visual Studio).

    Visual Studio: (Recommended) For building and running the C# Windows Forms application.

Installation and Setup
Kafka and Zookeeper

    Download Kafka: Download the latest stable release of Apache Kafka from https://kafka.apache.org/downloads.

    Extract: Extract the downloaded archive to a convenient location (e.g., H:\kafka).

    Start Zookeeper: Open a command prompt, navigate to your Kafka installation directory (e.g., H:\kafka\kafka_2.12-2.7.0), and run:

    .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties

    You should see output indicating Zookeeper is running.

    Start Kafka Server: Open a new command prompt, navigate to your Kafka installation directory, and run:

    .\bin\windows\kafka-server-start.bat .\config\server.properties

    You should see output indicating the Kafka server is running.

Creating Kafka Topic

Once Zookeeper and Kafka are running, create the topic for the chat messages:

    Open a new command prompt, navigate to your Kafka installation directory (e.g., H:\kafka\kafka_2.12-2.7.0\bin\windows).

    Execute the following command to create a topic named chat-message:

    kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic chat-message

    You should see a confirmation message like "Created topic chat-message."

How to Run
Kafka Console Producer

You can use a console producer to send messages to the chat-message topic:

    Open a new command prompt, navigate to H:\kafka\kafka_2.12-2.7.0\bin\windows.

    Run the producer command:

    kafka-console-producer.bat --broker-list localhost:9092 --topic chat-message

    Now, you can type messages and press Enter to send them. These messages will be consumed by the C# application and the console consumer.

Kafka Console Consumer

You can use a console consumer to view messages from the chat-message topic:

    Open a new command prompt, navigate to H:\kafka\kafka_2.12-2.7.0\bin\windows.

    Run the consumer command:

    kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic chat-message --from-beginning

    You will see messages appearing as they are sent by the C# application or the console producer.

C# Windows Forms Client

    Clone the repository or download the KafkaChatApp folder.

    Open in Visual Studio: Open the KafkaChatApp.sln solution file in Visual Studio.

    Restore NuGet Packages: Visual Studio should automatically restore the Confluent.Kafka NuGet package. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

    Build the Project: Build the KafkaChatApp project (Build > Build Solution).

    Run the Application: Press F5 or click the "Start" button in Visual Studio to run the Windows Forms application.

    The application will open, allowing you to type messages and send them. Received messages will appear in the display area.

Project Structure

KafkaChatApp/
├── .gitignore
├── README.md
├── KafkaChatApp.sln
└── WinFormsClient/
    ├── WinFormsClient.csproj
    ├── Form1.cs
    ├── Form1.Designer.cs
    ├── Program.cs
    └── App.config (or appsettings.json for .NET Core)

References

    Apache Kafka .NET Application

    Step by Step Installation and Configuration Guide of Apache Kafka on Windows Operating System

    Formatted and updated Through AI