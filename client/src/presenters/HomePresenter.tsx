import SearchBar from "../components/SearchBar"
import LoginCard from "../components/LoginCard"
import ContentContainer from "../components/ContentContainer";

export default function Home() {
    return (
        <ContentContainer>
            <div className="flex justify-center flex-col lg:flex-row">
                <div className="flex flex-col">
                    <HeaderSection />
                    <SearchBar />
                </div>
                <LoginCard />
            </div>
        </ContentContainer>
    );
}

export function HeaderSection() {
    return (
        <div className="bg-white py-4 sm:py-10">
            <div className="mx-auto max-w-7xl px-3 lg:px-6">
                <div className="mx-auto max-w-2xl lg:mx-0">
                    <a className="text-base font-semibold leading-7 text-indigo-600" href="https://uwflow.com">Inspired
                        by UW Flow</a>
                    <h2 className="mt-2 text-4xl font-bold tracking-tight text-gray-900 sm:text-6xl">Westernflow </h2>
                    <p className="mt-6 text-lg leading-8 text-gray-600">
                        A platform to centralize timetable information, course reviews, professor information, and more
                        for University of Western Ontario students.
                    </p>
                </div>
            </div>
        </div>
    )
}