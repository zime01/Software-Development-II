HotelEase Application

HotelEase is a full-stack application designed for hotel and flight reservations. The mobile app is intended for end-users to browse hotels, make reservations, and complete payments, while the desktop application is designed for hotel admins and managers to manage hotels, rooms, reservations, and users.

Note: The mobile project used is hotelease_mobile_new because the original hotelease_mobile project had issues with v2 embedding. The desktop application project remains hotelease_desktop.

The application is built using .NET 8 for the backend API and Flutter for the mobile frontend. Key technologies and integrations include:

Entity Framework Core (for database management)

RabbitMQ (for notifications and messaging)

SMTP (for sending registration confirmation emails and notifications)

Stripe (for payment processing)

Flutter (for mobile cross-platform frontend)

Prerequisites

Before setting up the environments, ensure you have:

Docker installed and running

RabbitMQ installed (for messaging)

Flutter installed

An Android Emulator like Android Studio
1. Running the Application

Clone the repository:
git clone https://github.com/zime01/Software-Development-II.git

2. Navigate to the backend folder:

Navigate to the backend folder:
cd Software-Development-II/backend/hotelEase

3. Unzip the .env files for both backend and frontend. Make sure they are placed in the same folder as their respective docker-compose.yml files.
4. Start the Docker environment with build:
docker-compose up --build

5. Credentials for testing:
username: admin password:test 
username: manager password:test 
username: desktop password:test 
username: mobile password:test 

Features

RabbitMQ + SMTP integration ensures successful email notifications on:

User registration

Completed reservations

Successful payments

Stripe integration allows secure payment processing for hotel reservations.

Admin & Manager dashboard on the desktop app for managing hotels, rooms, reservations, and notifications.

User-friendly mobile app for end-users to browse hotels, make reservations, and pay securely.
