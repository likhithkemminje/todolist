/*

    // Knockout.js ViewModel to manage tasks
    function TaskViewModel() {
        var self = this;
    self.UserId = null;
    self.tasks = ko.observableArray([]); // Array to hold tasks
    self.newTask = ko.observable(""); // New task input value

    self.tasksInRows = ko.computed(function () {
                var rows = [];
    for (var i = 0; i < self.tasks().length; i += 3) {
        rows.push(self.tasks().slice(i, i + 3)); // Group tasks into rows of 3
                }
    return rows;
            });


    // Observables for editing tasks
    self.isEditing = ko.observable(false); // To control visibility of the edit section
    self.editedTaskId = ko.observable(null); // ID of the task being edited
    self.editedTaskName = ko.observable(""); // Edited task name

    // Fetch tasks for the user
    self.fetchTasks = function (userId) {
        fetch(`/Task/GetTasksByUserId/${userId}`)
            .then(response => response.json())
            .then(data => {
                if (Array.isArray(data)) {
                    // Map the data into task objects with taskName, id, and status
                    self.tasks(data.map(task => ({
                        taskName: task.taskName,
                        id: task.id,
                        status: ko.observable(task.status) // Ensure status is an observable

                    })));
                }
            })
            .catch(error => console.error("Error fetching tasks:", error));

        };
    // Start editing a task
    self.startEditTask = function (task) {
        self.isEditing(true);
    self.editedTaskId(task.id);
    self.editedTaskName(task.taskName);
    $('#editTaskModal').modal('show');
        };
    // Cancel editing
    self.cancelEdit = function () {
        self.isEditing(false);
    self.editedTaskId(null);
    self.editedTaskName("");
    $('#editTaskModal').modal('hide'); // Close the modal
        };
    // Update a task
    self.updateTask = function (task) {
            var taskId = self.editedTaskId();
    var updatedTaskName = self.editedTaskName().trim();
    if (taskId && updatedTaskName) {
        fetch(`/Task/UpdateTask/${taskId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ id: taskId, taskName: updatedTaskName, userId: userId })
        })
            .then(response => {
                if (response.ok) {
                    // Update the task in the observable array

                    var taskToUpdate = self.tasks().find(task => task.id === taskId);
                    if (taskToUpdate) {
                        taskToUpdate.taskName = updatedTaskName;
                        self.fetchTasks(self.UserID);
                        // Notify Knockout about the update
                    }

                    // Clear editing state
                    self.cancelEdit();
                } else {
                    alert("Error updating task.");
                }
            })
            .catch(error => console.error("Error updating task:", error));
            } else {
        alert("Task name cannot be empty.");
            }
        };
    // Add a new task
    self.addTask = function () {
            var newTaskName = self.newTask().trim(); // Get the value of the new task
    if (newTaskName) {
        fetch(`/Task/PostTask/AddTasks`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ taskName: newTaskName, userId: userId })
        })
            .then(response => response.json())
            .then(data => {
                var newTask = {
                    taskName: data.taskName,
                    id: data.id,
                    status: ko.observable(false) // Default status is false (not done)
                };
                self.tasks.push(newTask);
                self.newTask(""); // Clear input
            })
            .catch(error => console.error('Error adding task:', error));
            } else {
        alert('Please enter a task.');
            }
        };

    // Toggle task status (Done/Undone)
    self.toggleTaskStatus = function (task) {
        fetch(`/Task/ToggleTaskStatus/toggle-status/${task.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" }
        })
            .then(response => {
                if (response.ok) {
                    // Update the task's status in the observable
                    console.log(task.status());
                    if (task.status()) {
                        task.status(false);  // This will trigger Knockout.js to apply the binding and strike-through

                    }
                    else {
                        task.status(true);
                    }

                    // Optionally, refetch tasks to make sure the UI is synchronized with the database
                    // self.fetchTasks(self.UserID);
                } else {
                    alert("Failed to update task status.");
                }
            })
            .catch(error => console.error("Error toggling task status:", error));
        };

    // Delete a task
    self.deleteTask = function (task) {
        fetch(`/Task/DeleteTask/${task.id}`, {
            method: 'DELETE'
        })
            .then(response => {
                if (response.ok) {
                    self.tasks.remove(task);
                } else {
                    alert('Error deleting task');
                }
            })
            .catch(error => console.error('Error deleting task:', error));
        };

    // Initialize and load tasks
    self.init = function (userId) {
        self.UserID = userId;
    self.fetchTasks(userId);
        };
    }

    // Initialize ViewModel and bind to view
    var viewModel = new TaskViewModel();
    var userId = @ViewData["UserId"];  // Dynamically pass userId from ViewData
    viewModel.init(userId);  // Fetch tasks for the user
    ko.applyBindings(viewModel); // Apply Knockout.js bindings

    // Logout function
    function logout() {
        if (confirm("Are you sure you want to log out?")) {
        window.location.href = "/User/Login"; // Redirect to login page
        }
    }
*/