-- ANSI SQL USING MYSQL EXERCISES SOLUTIONS

-- SCHEMA CREATION

CREATE DATABASE IF NOT EXISTS CommunityEventPortal;
USE CommunityEventPortal;

-- 1. Users Table
CREATE TABLE IF NOT EXISTS Users (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    city VARCHAR(100) NOT NULL,
    registration_date DATE NOT NULL
);

-- 2. Events Table
CREATE TABLE IF NOT EXISTS Events (
    event_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    city VARCHAR(100) NOT NULL,
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,
    status ENUM('upcoming', 'completed', 'cancelled') NOT NULL,
    organizer_id INT,
    FOREIGN KEY (organizer_id) REFERENCES Users(user_id)
);

-- 3. Sessions Table
CREATE TABLE IF NOT EXISTS Sessions (
    session_id INT PRIMARY KEY AUTO_INCREMENT,
    event_id INT,
    title VARCHAR(200) NOT NULL,
    speaker_name VARCHAR(100) NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);

-- 4. Registrations Table
CREATE TABLE IF NOT EXISTS Registrations (
    registration_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    event_id INT,
    registration_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);

-- 5. Feedback Table
CREATE TABLE IF NOT EXISTS Feedback (
    feedback_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    event_id INT,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comments TEXT,
    feedback_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);

-- 6. Resources Table
CREATE TABLE IF NOT EXISTS Resources (
    resource_id INT PRIMARY KEY AUTO_INCREMENT,
    event_id INT,
    resource_type ENUM('pdf', 'image', 'link') NOT NULL,
    resource_url VARCHAR(255) NOT NULL,
    uploaded_at DATETIME NOT NULL,
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);

-- SAMPLE DATA INSERTION

-- Insert Users
INSERT INTO Users (user_id, full_name, email, city, registration_date) VALUES
(1, 'Alice Johnson', 'alice@example.com', 'New York', '2024-12-01'),
(2, 'Bob Smith', 'bob@example.com', 'Los Angeles', '2024-12-05'),
(3, 'Charlie Lee', 'charlie@example.com', 'Chicago', '2024-12-10'),
(4, 'Diana King', 'diana@example.com', 'New York', '2025-01-15'),
(5, 'Ethan Hunt', 'ethan@example.com', 'Los Angeles', '2025-02-01');

-- Insert Events
INSERT INTO Events (event_id, title, description, city, start_date, end_date, status, organizer_id) VALUES
(1, 'Tech Innovators Meetup', 'A meetup for tech enthusiasts.', 'New York', '2025-06-10 10:00:00', '2025-06-10 16:00:00', 'upcoming', 1),
(2, 'AI & ML Conference', 'Conference on AI and ML advancements.', 'Chicago', '2025-05-15 09:00:00', '2025-05-15 17:00:00', 'completed', 3),
(3, 'Frontend Development Bootcamp', 'Hands-on training on frontend tech.', 'Los Angeles', '2025-07-01 10:00:00', '2025-07-03 16:00:00', 'upcoming', 2);

-- Insert Sessions
INSERT INTO Sessions (session_id, event_id, title, speaker_name, start_time, end_time) VALUES
(1, 1, 'Opening Keynote', 'Dr. Tech', '2025-06-10 10:00:00', '2025-06-10 11:00:00'),
(2, 1, 'Future of Web Dev', 'Alice Johnson', '2025-06-10 11:15:00', '2025-06-10 12:30:00'),
(3, 2, 'AI in Healthcare', 'Charlie Lee', '2025-05-15 09:30:00', '2025-05-15 11:00:00'),
(4, 3, 'Intro to HTML5', 'Bob Smith', '2025-07-01 10:00:00', '2025-07-01 12:00:00');

-- Insert Registrations
INSERT INTO Registrations (registration_id, user_id, event_id, registration_date) VALUES
(1, 1, 1, '2025-05-01'),
(2, 2, 1, '2025-05-02'),
(3, 3, 2, '2025-04-30'),
(4, 4, 2, '2025-04-28'),
(5, 5, 3, '2025-06-15');

-- Insert Feedback
INSERT INTO Feedback (feedback_id, user_id, event_id, rating, comments, feedback_date) VALUES
(1, 3, 2, 4, 'Great insights!', '2025-05-16'),
(2, 4, 2, 5, 'Very informative.', '2025-05-16'),
(3, 2, 1, 3, 'Could be better.', '2025-06-11');

-- Insert Resources
INSERT INTO Resources (resource_id, event_id, resource_type, resource_url, uploaded_at) VALUES
(1, 1, 'pdf', 'https://portal.com/resources/tech_meetup_agenda.pdf', '2025-05-01 10:00:00'),
(2, 2, 'image', 'https://portal.com/resources/ai_poster.jpg', '2025-04-20 09:00:00'),
(3, 3, 'link', 'https://portal.com/resources/html5_docs', '2025-06-25 15:00:00');


-- EXERCISE QUERIES

-- Exercise 1: User Upcoming Events
-- Show a list of all upcoming events a user is registered for in their city, sorted by date.
SELECT e.event_id, e.title, e.start_date, u.full_name
FROM Registrations r
JOIN Events e ON r.event_id = e.event_id
JOIN Users u ON r.user_id = u.user_id
WHERE e.status = 'upcoming' AND e.city = u.city
ORDER BY e.start_date ASC;

-- Exercise 2: Top Rated Events
-- Identify events with the highest average rating, considering only those that have received at least 10 feedback submissions.
-- (Note: In the sample dataset we have fewer than 10, so this query will show empty unless threshold is lowered, but complies with query spec)
SELECT e.event_id, e.title, AVG(f.rating) AS avg_rating, COUNT(f.feedback_id) AS feedback_count
FROM Events e
JOIN Feedback f ON e.event_id = f.event_id
GROUP BY e.event_id, e.title
HAVING COUNT(f.feedback_id) >= 10
ORDER BY avg_rating DESC;

-- Exercise 3: Inactive Users
-- Retrieve users who have not registered for any events in the last 90 days.
SELECT u.user_id, u.full_name, u.email
FROM Users u
WHERE u.user_id NOT IN (
    SELECT DISTINCT r.user_id
    FROM Registrations r
    WHERE r.registration_date >= DATE_SUB(CURDATE(), INTERVAL 90 DAY)
);

-- Exercise 4: Peak Session Hours
-- Count how many sessions are scheduled between 10 AM to 12 PM for each event.
SELECT event_id, COUNT(session_id) AS peak_sessions_count
FROM Sessions
WHERE TIME(start_time) >= '10:00:00' AND TIME(end_time) <= '12:00:00'
GROUP BY event_id;

-- Exercise 5: Most Active Cities
-- List the top 5 cities with the highest number of distinct user registrations.
SELECT e.city, COUNT(DISTINCT r.user_id) AS distinct_users_count
FROM Registrations r
JOIN Events e ON r.event_id = e.event_id
GROUP BY e.city
ORDER BY distinct_users_count DESC
LIMIT 5;

-- Exercise 6: Event Resource Summary
-- Generate a report showing the number of resources (PDFs, images, links) uploaded for each event.
SELECT e.event_id, e.title,
       SUM(CASE WHEN r.resource_type = 'pdf' THEN 1 ELSE 0 END) AS pdf_count,
       SUM(CASE WHEN r.resource_type = 'image' THEN 1 ELSE 0 END) AS image_count,
       SUM(CASE WHEN r.resource_type = 'link' THEN 1 ELSE 0 END) AS link_count,
       COUNT(r.resource_id) AS total_resources
FROM Events e
LEFT JOIN Resources r ON e.event_id = r.event_id
GROUP BY e.event_id, e.title;

-- Exercise 7: Low Feedback Alerts
-- List all users who gave feedback with a rating less than 3, along with their comments and associated event names.
SELECT u.full_name, f.rating, f.comments, e.title AS event_name
FROM Feedback f
JOIN Users u ON f.user_id = u.user_id
JOIN Events e ON f.event_id = e.event_id
WHERE f.rating < 3;

-- Exercise 8: Sessions per Upcoming Event
-- Display all upcoming events with the count of sessions scheduled for them.
SELECT e.event_id, e.title, COUNT(s.session_id) AS session_count
FROM Events e
LEFT JOIN Sessions s ON e.event_id = s.event_id
WHERE e.status = 'upcoming'
GROUP BY e.event_id, e.title;

-- Exercise 9: Organizer Event Summary
-- For each event organizer, show the number of events created and their current status (upcoming, completed, cancelled).
SELECT u.user_id, u.full_name, e.status, COUNT(e.event_id) AS event_count
FROM Users u
JOIN Events e ON u.user_id = e.organizer_id
GROUP BY u.user_id, u.full_name, e.status;

-- Exercise 10: Feedback Gap
-- Identify events that had registrations but received no feedback at all.
SELECT e.event_id, e.title
FROM Events e
WHERE e.event_id IN (SELECT DISTINCT event_id FROM Registrations)
  AND e.event_id NOT IN (SELECT DISTINCT event_id FROM Feedback);

-- Exercise 11: Daily New User Count
-- Find the number of users who registered each day in the last 7 days.
SELECT registration_date, COUNT(user_id) AS new_users_count
FROM Users
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
GROUP BY registration_date;

-- Exercise 12: Event with Maximum Sessions
-- List the event(s) with the highest number of sessions.
SELECT event_id, COUNT(session_id) AS session_count
FROM Sessions
GROUP BY event_id
HAVING COUNT(session_id) = (
    SELECT MAX(session_cnt)
    FROM (SELECT COUNT(session_id) AS session_cnt FROM Sessions GROUP BY event_id) AS temp
);

-- Exercise 13: Average Rating per City
-- Calculate the average feedback rating of events conducted in each city.
SELECT e.city, AVG(f.rating) AS avg_rating
FROM Events e
JOIN Feedback f ON e.event_id = f.event_id
GROUP BY e.city;

-- Exercise 14: Most Registered Events
-- List top 3 events based on the total number of user registrations.
SELECT e.event_id, e.title, COUNT(r.registration_id) AS registration_count
FROM Events e
JOIN Registrations r ON e.event_id = r.event_id
GROUP BY e.event_id, e.title
ORDER BY registration_count DESC
LIMIT 3;

-- Exercise 15: Event Session Time Conflict
-- Identify overlapping sessions within the same event (i.e., session start and end times that conflict).
SELECT s1.event_id, s1.title AS session1, s2.title AS session2, s1.start_time, s1.end_time
FROM Sessions s1
JOIN Sessions s2 ON s1.event_id = s2.event_id AND s1.session_id < s2.session_id
WHERE (s1.start_time < s2.end_time AND s1.end_time > s2.start_time);

-- Exercise 16: Unregistered Active Users
-- Find users who created an account in the last 30 days but haven’t registered for any events.
SELECT user_id, full_name, email, registration_date
FROM Users
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
  AND user_id NOT IN (SELECT DISTINCT user_id FROM Registrations);

-- Exercise 17: Multi-Session Speakers
-- Identify speakers who are handling more than one session across all events.
SELECT speaker_name, COUNT(session_id) AS session_count
FROM Sessions
GROUP BY speaker_name
HAVING COUNT(session_id) > 1;

-- Exercise 18: Resource Availability Check
-- List all events that do not have any resources uploaded.
SELECT event_id, title
FROM Events
WHERE event_id NOT IN (SELECT DISTINCT event_id FROM Resources);

-- Exercise 19: Completed Events with Feedback Summary
-- For completed events, show total registrations and average feedback rating.
SELECT e.event_id, e.title, 
       COUNT(DISTINCT r.registration_id) AS total_registrations, 
       AVG(f.rating) AS avg_rating
FROM Events e
LEFT JOIN Registrations r ON e.event_id = r.event_id
LEFT JOIN Feedback f ON e.event_id = f.event_id
WHERE e.status = 'completed'
GROUP BY e.event_id, e.title;

-- Exercise 20: User Engagement Index
-- For each user, calculate how many events they attended (completed events registered for) and how many feedbacks they submitted.
SELECT u.user_id, u.full_name,
       COUNT(DISTINCT CASE WHEN e.status = 'completed' THEN r.registration_id END) AS events_attended,
       COUNT(DISTINCT f.feedback_id) AS feedbacks_submitted
FROM Users u
LEFT JOIN Registrations r ON u.user_id = r.user_id
LEFT JOIN Events e ON r.event_id = e.event_id
LEFT JOIN Feedback f ON u.user_id = f.user_id AND f.event_id = e.event_id
GROUP BY u.user_id, u.full_name;

-- Exercise 21: Top Feedback Providers
-- List top 5 users who have submitted the most feedback entries.
SELECT u.user_id, u.full_name, COUNT(f.feedback_id) AS feedback_count
FROM Users u
JOIN Feedback f ON u.user_id = f.user_id
GROUP BY u.user_id, u.full_name
ORDER BY feedback_count DESC
LIMIT 5;

-- Exercise 22: Duplicate Registrations Check
-- Detect if a user has been registered more than once for the same event.
SELECT user_id, event_id, COUNT(registration_id) AS registration_count
FROM Registrations
GROUP BY user_id, event_id
HAVING COUNT(registration_id) > 1;

-- Exercise 23: Registration Trends
-- Show a month-wise registration count trend over the past 12 months.
SELECT DATE_FORMAT(registration_date, '%Y-%m') AS reg_month, COUNT(registration_id) AS registration_count
FROM Registrations
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 12 MONTH)
GROUP BY reg_month
ORDER BY reg_month ASC;

-- Exercise 24: Average Session Duration per Event
-- Compute the average duration (in minutes) of sessions in each event.
SELECT event_id, AVG(TIMESTAMPDIFF(MINUTE, start_time, end_time)) AS avg_session_duration_minutes
FROM Sessions
GROUP BY event_id;

-- Exercise 25: Events Without Sessions
-- List all events that currently have no sessions scheduled under them.
SELECT event_id, title
FROM Events
WHERE event_id NOT IN (SELECT DISTINCT event_id FROM Sessions);