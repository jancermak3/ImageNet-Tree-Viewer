# ImageNet Tree Viewer

This application provides a tree view of the ImageNet taxonomy system, allowing users to explore and search the hierarchical structure of image categories.

## Features

- Tree view of ImageNet taxonomy
- Search functionality to filter tree nodes
- RESTful API for retrieving tree data

## Technologies Used

- Backend: .NET 8, C#
- Frontend: React, TypeScript
- Database: Entity Framework Core
- Docker for containerization

## Getting Started

### Prerequisites

- Docker
- Docker Compose

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/jancermak3/ImageNet-Tree-Viewer.git
   ```
2. Navigate to the project directory:
   ```
   cd ImageNet-Tree-Viewer
   ```
3. Build and start the containers:
   ```
   docker-compose up --build
   ```

The application should now be running at `http://localhost:3000` for the frontend and `http://localhost:5248` for the backend API.

## Description

The web application is consisting of just one page which shows the tree view of the data that was fetched from the ImageNet web in a xml format. During the startup the application will fetch the data from the ImageNet web, parse it into linear format and save it to the database. The data is being read from the database and transformed into a tree structure for the frontend using a REST API.

## API Documentation

There are only 2 endpoints:

1. GET /api/imagenetitems/tree

Returns all of the tree data in JSON format.

2. GET /api/imagenetitems/linear

Returns all of the tree data in a JSON linear format.