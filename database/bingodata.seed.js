var bingoDb = conn.getDB("bingo");
// Append this document with seed data.

bingoDb.exercises.save([
        {
            "_id": ObjectId("5a37c78f977cd7e92241b9b9"),
            "Name": "Bench Press",
            "ShortName": "Bench",
            "LongName": "Flat Barbell Bench Press"
        },
        {
            "_id": ObjectId("5a37c78f977cd7e92241b9ba"),
            "Name": "Barbell Squat",
            "ShortName": "Squat",
            "LongName": "Barbell Back Squat"
        },
        {
            "_id": ObjectId("5a37c78f977cd7e92241b9bb"),
            "Name": "Barbell Deadlift",
            "ShortName": "Deadlift",
            "LongName": "Conventional Barbell Deadlift"
        }
    ]);

bingoDb.exercises.createIndex({ 'Name': 1 }, {
        name: "Index_Unique_Name",
        collation: { locale: "en", strength: 2 },
        unique: true
    });