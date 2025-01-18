import React, { useState } from "react";
import axios from "axios";
import pendingImage from "../../assets/pendingButton.png";
import completeImage from "../../assets/completeButton.png";
import allImage from "../../assets/allButton.png";
import newTaskImage from "../../assets/newTaskButton.png";

const Filters = ({ onFilterChange, filter }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [newTaskName, setNewTaskName] = useState("");
  const [newTaskDescription, setNewTaskDescription] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleFilterChange = (newFilter) => {
    onFilterChange(newFilter);
  };

  const getToken = async () => {
    try {
      const tokenResponse = await axios.post("/api/auth", {

        username: "unowmyusername",

        password: "unowmypassword",

      });

      return tokenResponse.data.body;

    } catch (error) {

      console.error("Error fetching token:", error);

      alert("Authentication failed. Please check your credentials.");

      throw error;
    }
  };

  const handleCreateTask = async () => {

    let response;

    try {

      setIsSubmitting(true);

      const token = await getToken(); 

      response = await axios.post("/api/task",
        {description: newTaskDescription },
        {
          headers: {
            Authorization: `Bearer ${token}`, 
          },
        }
      );

      console.log("Task Created:", response.data.body);

      setNewTaskName("");

      setNewTaskDescription("");

      setIsModalOpen(false);

    } catch (error) {

      alert(error);

    } finally {

      setIsSubmitting(false);
    }
  };

  return (
    <div>
      {/* Modal for creating a new task */}
      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
          <div className="bg-white p-6 rounded-lg shadow-lg w-1/3">
            <h2 className="text-lg font-bold mb-4">Create New Task</h2>
            <textarea
              value={newTaskDescription}
              onChange={(e) => setNewTaskDescription(e.target.value)}
              className="w-full p-2 border border-gray-300 rounded mb-4"
              placeholder="Enter task description"
              rows="4"
            ></textarea>
            <div className="flex justify-end space-x-4">
              <button
                className="px-4 py-2 bg-gray-500 text-white rounded-lg"
                onClick={() => setIsModalOpen(false)}
                disabled={isSubmitting}
              >
                Cancel
              </button>
              <button
                className="px-4 py-2 bg-blue-500 text-white rounded-lg"
                onClick={handleCreateTask}
                disabled={isSubmitting}
              >
                {isSubmitting ? "Creating..." : "Create"}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Filter buttons */}
      <div className="fixed bottom-0 left-0 right-0 p-4 bg-coally-dark-blue shadow-lg flex justify-between items-center rounded-t-xl">
        <div className="flex space-x-4">
          <button
            className={`px-4 py-2 rounded-lg text-sm font-semibold transition-colors flex items-center space-x-2 ${
              filter === "pending"
                ? "bg-blue-500 text-white"
                : "bg-gray-200 text-gray-700"
            } hover:bg-blue-600 hover:text-white`}
            onClick={() => handleFilterChange("pending")}
          >
            <img src={pendingImage} alt="Pending" className="w-5 h-5" />
            <span>Pending</span>
          </button>
          <button
            className={`px-4 py-2 rounded-lg text-sm font-semibold transition-colors flex items-center space-x-2 ${
              filter === "completed"
                ? "bg-green-500 text-white"
                : "bg-gray-200 text-gray-700"
            } hover:bg-green-600 hover:text-white`}
            onClick={() => handleFilterChange("completed")}
          >
            <img src={completeImage} alt="Completed" className="w-5 h-5" />
            <span>Completed</span>
          </button>
          <button
            className={`px-4 py-2 rounded-lg text-sm font-semibold transition-colors flex items-center space-x-2 ${
              filter === "all"
                ? "bg-yellow-500 text-white"
                : "bg-gray-200 text-gray-700"
            } hover:bg-yellow-600 hover:text-white`}
            onClick={() => handleFilterChange("all")}
          >
            <img src={allImage} alt="All" className="w-5 h-5" />
            <span>All</span>
          </button>
        </div>

        {/* Create Task Button */}
        <button
          className="bg-coally-orange text-white p-4 rounded-lg shadow-lg flex items-center space-x-2"
          onClick={() => setIsModalOpen(true)}
        >
          <img src={newTaskImage} alt="New Task" className="w-5 h-5" />
          <span>Create Task</span>
        </button>
      </div>
    </div>
  );
};

export default Filters;
