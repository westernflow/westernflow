import React, { ReactNode } from 'react';

interface ContentContainerProps {
    children: ReactNode;
    additionalClasses?: string;
}

export default function ContentContainer({ children, additionalClasses }: ContentContainerProps) {
    return <div className={"mx-auto max-w-7xl sm:px-6 lg:px-8 min-h-screen " + additionalClasses}> {children} </div>;
}
