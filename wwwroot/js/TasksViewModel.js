function TasksViewModel(userId) {
    var self = this;

    // Observable array for tasks
    self.tasks = ko.observableArray([]);

    // Fetch tasks for the logged-in user
    self.fetchTasks = function () {
        $.ajax({
            url: `/Task/GetTasksByUser?userId=${userId}`, // Backend API to fetch tasks
            type: "GET",
            success: function (response) {
                self.tasks(response); // Bind tasks to observable array
            },
            error: function () {
                alert("Failed to fetch tasks. Please try again.");
            }
        });
    };

    // Fetch tasks on initialization
    self.fetchTasks();
}

// Apply bindings for tasks page
$(document).ready(function () {
    // Assume `loggedInUserId` is retrieved from session or a hidden field
    var loggedInUserId = $("#LoggedInUserId").val();
    ko.applyBindings({ tasksViewModel: new TasksViewModel(loggedInUserId) });
});
