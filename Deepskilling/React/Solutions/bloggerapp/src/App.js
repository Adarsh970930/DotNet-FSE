import React, { useState } from 'react';
import './App.css';
import CourseDetails from './components/CourseDetails';
import BookDetails from './components/BookDetails';
import BlogDetails from './components/BlogDetails';

function App() {
  const [showCourse, setShowCourse] = useState(true);
  const [showBook, setShowBook] = useState(true);
  const [showBlog, setShowBlog] = useState(true);

  return (
    <div className="App">
      <div className="container">

        {/* Conditional Rendering using && */}
        {showCourse && (
          <div className="box">
            <CourseDetails />
          </div>
        )}

        {/* Conditional Rendering using ternary operator */}
        {showBook ? (
          <div className="box">
            <BookDetails />
          </div>
        ) : null}

        {/* Conditional Rendering using IIFE */}
        {(() => {
          if (showBlog) {
            return (
              <div className="box">
                <BlogDetails />
              </div>
            );
          }
        })()}

      </div>
    </div>
  );
}

export default App;