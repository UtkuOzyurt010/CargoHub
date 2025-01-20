#!/bin/bash
# This script is for copying current active DataBase to TestEnvironment and running all tests.
# Set the project root directory dynamically
PROJECT_ROOT="$(pwd)"

# Define the source and destination paths relative to the project root
SOURCE_DB="$PROJECT_ROOT/CargoHub.sqlite"
DEST_DIR="$PROJECT_ROOT.Tests/bin/Debug/net8.0"
DEST_DB="$DEST_DIR/CargoHub.sqlite"

# Check if the source database exists
if [ -f "$SOURCE_DB" ]; then
    echo "Source database found at $SOURCE_DB"
else
    echo "Source database not found at $SOURCE_DB"
    exit 1
fi

# Check if the destination directory exists, and create it if it doesn't
if [ ! -d "$DEST_DIR" ]; then
    echo "Destination directory $DEST_DIR does not exist. Creating it..."
    mkdir -p "$DEST_DIR"
fi

# Remove the existing database in the destination directory if it exists
if [ -f "$DEST_DB" ]; then
    echo "Removing existing database at $DEST_DB"
    rm "$DEST_DB"
fi

# Copy the database from source to destination
cp "$SOURCE_DB" "$DEST_DB"

# Check if the copy was successful
if [ -f "$DEST_DB" ]; then
    echo "Database successfully copied to $DEST_DB"
else
    echo "Failed to copy the database."
    exit 1
fi

echo "Running dotnet test..."
echo ls 
#dotnet test "../"
dotnet test "../" --logger "trx;LogFileName=test_results.trx"
