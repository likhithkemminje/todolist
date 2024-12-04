function LoginViewModel() {
    var self = this;

    // Observables for form fields
    self.email = ko.observable('');
    self.password = ko.observable('');

    // Observables for validation error messages
    self.emailError = ko.observable('');
    self.passwordError = ko.observable('');
    self.errorMessage = ko.observable('');

    // Validation functions
    self.validateEmail = function () {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Simple email format regex
        if (!self.email()) {
            self.emailError('Email is required.');
        } else if (!emailRegex.test(self.email())) {
            self.emailError('Please enter a valid email address.');
        } else {
            self.emailError('');
        }
    };

    self.validatePassword = function () {
        if (!self.password()) {
            self.passwordError('Password is required.');
        } else if (self.password().length < 6) {
            self.passwordError('Password must be at least 6 characters long.');
        } else {
            self.passwordError('');
        }
    };

    // Login function
    self.loginUser = function () {
        self.errorMessage(''); // Clear previous error messages
        self.validateEmail();
        self.validatePassword();

        // Stop if there are validation errors
        if (self.emailError() || self.passwordError()) {
            return;
        }

        // Perform AJAX POST request to login API
        $.ajax({
            url: '/User/Login/Login', // Backend login API endpoint
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Email: self.email(),
                Password: self.password()
            }),
            success: function (response) {
                // Redirect to task page on successful login
                window.location.href = '/Task/Viewtask'; // Change this URL to your desired route
            },
            error: function (xhr) {
                // Show error message
                self.errorMessage(xhr.responseText || 'Login failed. Please try again.');
            }
        });
    };
}

// Apply bindings after DOM is ready
$(document).ready(function () {
    ko.applyBindings({ loginViewModel: new LoginViewModel() });
});