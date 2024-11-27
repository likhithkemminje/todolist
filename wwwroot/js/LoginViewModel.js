/*function LoginViewModel() {
    var self = this;

    // Observables for form fields
    self.email = ko.observable("");
    self.password = ko.observable("");
    self.errorMessage = ko.observable("");

    // Login function
    self.loginUser = function () {
        self.errorMessage(""); // Clear error message

        // Validate form inputs
        if (!self.email() || !self.password()) {
            self.errorMessage("Both email and password are required.");
            return;
        }

        // Perform AJAX POST request to login API
        $.ajax({
            url: "/User/Login", // Backend login API endpoint
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Email: self.email(),
                Password: self.password()
            }),
            success: function (response) {
                // Redirect to task page on successful login
                window.location.href = "/Task/UserTasks";
            },
            error: function (xhr) {
                // Show error message
                self.errorMessage(xhr.responseText || "Login failed. Please try again.");
            }
        });
    };
}

// Apply bindings
$(document).ready(function () {
    ko.applyBindings({ loginViewModel: new LoginViewModel() });
});
*/