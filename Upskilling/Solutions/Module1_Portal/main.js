$(document).ready(function () {
  console.log("Welcome to the Community Portal");

  // ALERT ON FULL LOAD
  window.addEventListener('load', () => {
    console.log("Portal Page Fully Loaded");
  });

  // MOCK EVENT DATABASE
  let events = [
    { id: 1, title: "Tech Innovators Meetup", category: "tech", seats: 10, date: "2025-06-10", fee: "$10" },
    { id: 2, title: "AI & ML Conference", category: "tech", seats: 0, date: "2025-05-15", fee: "$50" },
    { id: 3, title: "Summer Concert Live", category: "music", seats: 120, date: "2025-08-20", fee: "$15" },
    { id: 4, title: "Frontend Bootcamp", category: "workshop", seats: 25, date: "2025-07-01", fee: "$25" }
  ];

  // RETRIEVE SAVED PREFERENCE
  const savedCategory = localStorage.getItem('preferredCategory');
  if (savedCategory) {
    $('#categoryFilter').val(savedCategory);
  }

  // RENDER EVENTS DYNAMICALLY
  function renderEvents() {
    $('#loadingSpinner').show();
    $('#eventsGrid').empty();

    const selectedCat = $('#categoryFilter').val();
    const searchVal = $('#quickSearch').val().toLowerCase();

    // Filter list
    let filtered = events.filter(e => {
      let matchCat = (selectedCat === 'all' || e.category === selectedCat);
      let matchSearch = e.title.toLowerCase().includes(searchVal);
      return matchCat && matchSearch;
    });

    setTimeout(() => {
      $('#loadingSpinner').hide();
      if (filtered.length === 0) {
        $('#eventsGrid').append('<div class="col-12 text-center text-muted"><p>No matching events found.</p></div>');
        return;
      }

      filtered.forEach(e => {
        let isFull = e.seats === 0;
        let cardHtml = `
          <div class="col-md-6 col-lg-4 event-card-container">
            <div class="card h-100 ${isFull ? 'bg-light border-danger' : 'shadow-sm'}">
              <div class="card-body">
                <span class="badge bg-secondary mb-2">${e.category.toUpperCase()}</span>
                <h5 class="card-title fw-bold">${e.title}</h5>
                <p class="card-text text-muted mb-1"><i class="bi bi-calendar3"></i> Date: ${e.date}</p>
                <p class="card-text text-muted mb-3"><i class="bi bi-ticket-perforated"></i> Entry Fee: ${e.fee}</p>
                
                <div class="d-flex justify-content-between align-items-center">
                  <span class="text-sm fw-bold ${isFull ? 'text-danger' : 'text-success'}">
                    ${isFull ? 'SOLD OUT' : e.seats + ' Seats Left'}
                  </span>
                  <button class="btn btn-sm btn-primary register-btn" data-fee="${e.fee}" data-title="${e.title}" ${isFull ? 'disabled' : ''}>
                    Register
                  </button>
                </div>
              </div>
            </div>
          </div>
        `;
        $('#eventsGrid').append(cardHtml);
      });
      
      // Fade in animations for cards
      $('.event-card-container').hide().fadeIn(500);

    }, 300);
  }

  // FILTER TRIGGERS
  $('#categoryFilter').change(function() {
    const val = $(this).val();
    localStorage.setItem('preferredCategory', val);
    renderEvents();
  });

  $('#quickSearch').on('keydown input', function() {
    renderEvents();
  });

  // INITIAL RENDER
  renderEvents();

  // DYNAMIC REGISTER CLICK
  $(document).on('click', '.register-btn', function() {
    const fee = $(this).attr('data-fee');
    const title = $(this).attr('data-title');
    $('#floatingEvent option').each(function() {
      if ($(this).text().includes(title)) {
        $(this).prop('selected', true);
      }
    });
    $('#eventFeeText').text(fee);
    $('html, body').animate({
      scrollTop: $("#register").offset().top - 100
    }, 500);
  });

  // PREFERENCE CLEAR
  $('#clearPrefBtn').click(function() {
    localStorage.clear();
    sessionStorage.clear();
    $('#categoryFilter').val('all');
    renderEvents();
    alert('Local and Session storage preferences cleared!');
  });

  // INTERACTIVE SANDBOX (COLORBOX EVENTS)
  $('#colorBtn').click(function() {
    $('#colorBox').css('background-color', '#ffc107').text('Box Painted Yellow!');
  });

  $('#colorBox').dblclick(function() {
    $(this).css('background-color', '#ffffff').text('Box Reset to White!');
  });

  $('#colorBox').hover(
    function() {
      $(this).addClass('highlight-element');
    },
    function() {
      $(this).removeClass('highlight-element');
    }
  );

  // PAST GALLERY DOUBLE CLICK TO ENLARGE
  $('.gallery-img').dblclick(function() {
    $(this).toggleClass('gallery-img-large');
  });

  // REGISTRATION FORM SUBMISSION
  $('#regForm').submit(function(event) {
    event.preventDefault();
    const name = $('#floatingName').val();
    const email = $('#floatingEmail').val();
    
    // Save registered email in sessionStorage
    sessionStorage.setItem('lastRegisteredEmail', email);

    $('#regOutput').text(`Registration confirmed for ${name}. Details sent to ${email}!`);
    $('#regAlert').removeClass('d-none').hide().fadeIn(300);
    
    // Increment or Decrement seats test simulation
    events[0].seats--; 
    renderEvents();
  });

  // PHONE VALIDATION ON BLUR
  $('#floatingPhone').blur(function() {
    const val = $(this).val();
    const regex = /^\d{10}$/;
    if (!regex.test(val)) {
      $('#phoneFeedback').text('Please enter a valid 10-digit phone number.');
    } else {
      $('#phoneFeedback').empty();
    }
  });

  // EVENT DROPDOWN CHANGE
  $('#floatingEvent').change(function() {
    const selectedText = $(this).find('option:selected').text();
    const feeMatch = selectedText.match(/\$\d+/);
    if (feeMatch) {
      $('#eventFeeText').text(feeMatch[0]);
    }
  });

  // FEEDBACK TEXTAREA KEYDOWN CHAR COUNT
  $('#feedbackText').on('keyup keydown change', function() {
    const len = $(this).val().length;
    $('#charCount').text(len);
  });

  // VIDEO CANPLAY EVENT
  const video = document.getElementById('promoVideo');
  if (video) {
    video.oncanplay = function() {
      $('#videoStatus').text('Promo Video Ready to Play!').addClass('text-success');
    };
  }

  // GEOLOCATION getCurrentPosition
  $('#findLocationBtn').click(function() {
    if (!navigator.geolocation) {
      $('#locationOutput').text('Geolocation is not supported by your browser.');
      return;
    }

    $('#locationOutput').html('<div class="spinner-border spinner-border-sm text-primary" role="status"></div> Requesting position...');

    const options = {
      enableHighAccuracy: true,
      timeout: 5000,
      maximumAge: 0
    };

    navigator.geolocation.getCurrentPosition(
      (pos) => {
        const lat = pos.coords.latitude.toFixed(4);
        const lon = pos.coords.longitude.toFixed(4);
        $('#locationOutput').html(`<strong>Coordinates found:</strong> Latitude: ${lat}, Longitude: ${lon} (Accuracy: ${pos.coords.accuracy}m)`);
      },
      (err) => {
        let msg = 'Error obtaining location: ';
        if (err.code === 1) msg += 'Permission denied.';
        else if (err.code === 2) msg += 'Position unavailable.';
        else if (err.code === 3) msg += 'Request timeout.';
        $('#locationOutput').text(msg).addClass('text-danger');
      },
      options
    );
  });

  // JQUERY NOTICE BOARD (LIST ADDITIONS)
  $('#noticeForm').submit(function(e) {
    e.preventDefault();
    const text = $('#noticeInput').val();
    if (text) {
      const newItem = $(`
        <li class="list-group-item d-flex justify-content-between align-items-center bg-light">
          ${text}
          <button class="btn btn-sm btn-danger remove-notice-btn">Delete</button>
        </li>
      `);
      $('#noticeList').append(newItem);
      $('#noticeInput').val('');
    }
  });

  // REMOVE CUSTOM NOTICE
  $(document).on('click', '.remove-notice-btn', function() {
    $(this).closest('li').remove();
  });

  // REMOVE ALL NOTICES
  $('#clearAllNotices').click(function() {
    $('#noticeList').empty();
  });

  // BEFORE UNLOAD WARN
  window.addEventListener('beforeunload', function(e) {
    const formFilled = $('#floatingName').val() || $('#floatingEmail').val();
    if (formFilled) {
      e.preventDefault();
      e.returnValue = 'You have unsaved changes. Leave?';
    }
  });

});