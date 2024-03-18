# Online Dictionary App

This is a simple online dictionary app.

## Prerequisites

Before running the app, make sure you have the following installed:

- [Docker](https://www.docker.com/get-started)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Dev Containers VSC Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)

## Getting Started
To run the app locally using a devcontainer, follow these steps:

1. Clone this repository to your local machine:

   ```bash
   git clone <repository_url>
   ```
2. Open Visual Studio Code and navigate to the project directory:
   You should open the project's folder as a VSC Workspace.

    ```bash
    cd online-dictionary
    code .
    ```
4. Visual Studio Code will prompt you to reopen the project in a container. Click "Reopen in Container" to open the project in a devcontainer.
5. Once the devcontainer is initialized, you can build and run the app using the integrated terminal in Visual Studio Code.

    ```bash
    # Build the app
    dotnet build
    
    # Run the app
    dotnet run
    ```
Access the app in your web browser at http://localhost:5052.

## Development
You can make changes to the code using any text editor or IDE.
Changes made to the code will be automatically reflected in the running app.
Press Ctrl+C in the terminal to stop the app.
