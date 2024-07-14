const GradientBackground = () => {
    return (
        <div className="min-h-screen bg-wf flex flex-col justify-center items-center">
            <header className="text-center">
                <h1 className="text-5xl font-bold text-blue-700">
                    Westernflow
                </h1>
                <p className="text-lg text-gray-600 mt-2">
                    A platform to centralize timetable information, course
                    reviews, professor information, and more for University of
                    Western Ontario students.
                </p>
                <div className="mt-4">
                    <input  
                        type="text"
                        placeholder="Search for courses, professors, or subjects"
                        className="px-4 py-2 rounded-full border border-gray-300 focus:outline-none"
                    />
                </div>
                <button className="mt-4 px-6 py-2 bg-blue-600 text-white rounded-full hover:bg-blue-700 focus:outline-none">
                    Explore all courses and professors
                </button>
            </header>
        </div>
    );
};

export default GradientBackground;
