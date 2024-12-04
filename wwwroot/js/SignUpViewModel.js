function SignUpViewModel() {
    var self = this;

    // Observables for form fields
    self.email = ko.observable("");
    self.password = ko.observable("");
    self.username = ko.observable("");

    // Observables for validation error messages
    self.emailError = ko.observable("");
    self.passwordError = ko.observable("");
    self.usernameError = ko.observable("");
    self.errorMessage = ko.observable("");

    // Validation functions
    self.validateEmail = function () {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Basic email format regex
        if (!self.email()) {
            self.emailError("Email is required.");
        } else if (!emailRegex.test(self.email())) {
            self.emailError("Please enter a valid email address.");
        } else {
            self.emailError("");
        }
    };

    self.validateUsername = function () {
        if (!self.username()) {
            self.usernameError("Username is required.");
        } else if (self.username().length < 3) {
            self.usernameError("Username must be at least 3 characters long.");
        } else {
            self.usernameError("");
        }
    };

    self.validatePassword = function () {
        if (!self.password()) {
            self.passwordError("Password is required.");
        } else if (self.password().length < 6) {
            self.passwordError("Password must be at least 6 characters long.");
        } else {
            self.passwordError("");
        }
    };

    // Signup function
    self.signupUser = function () {
        self.errorMessage(""); // Clear global error message
        self.validateEmail();
        self.validateUsername();
        self.validatePassword();

        // Stop submission if any validation errors exist
        if (self.emailError() || self.usernameError() || self.passwordError()) {
            return;
        }

        // Perform AJAX POST request to signup API
        $.ajax({
            url: "/User/PostUser/SignUp", // Adjust endpoint as necessary
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Email: self.email(),
                PasswordHash: self.password(), // Backend expects PasswordHash
                Username: self.username()
            }),
            success: function (response) {
                alert("Signup successful!");
                window.location.href = "/User/Login"; // Redirect to login page
            },
            error: function (xhr) {
                if (xhr.status === 409) {
                    // Handle specific "Email already exists" response
                    self.errorMessage("Email already exists. Please use another email ID.");
                } else {
                    // Handle other errors
                    self.errorMessage(xhr.responseJSON?.title || "Signup failed. Please try again.");
                }
            }
        });
    };
}

// Apply Knockout bindings when the DOM is ready
$(document).ready(function () {
    ko.applyBindings({ signupViewModel: new SignUpViewModel() });
});
