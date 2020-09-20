CREATE TABLE "User" (
  "Id" SERIAL PRIMARY KEY,
  "Name" VARCHAR(100) NOT NULL,
  "Email" VARCHAR(100) NOT NULL,
  "Password" VARCHAR(100) NOT NULL
);

CREATE TABLE "TodoItem" (
  "Id" SERIAL PRIMARY KEY,
  "Description" VARCHAR(300) NOT NULL,
  "Done" BOOLEAN,
  "UserId" INT NOT NULL REFERENCES "User"("Id")
);

INSERT INTO "User" ("Name", "Email", "Password") 
  VALUES
('admin', 'admin@mail.com', 'admin123'),
('contact', 'contact@mail.com', 'contact123');