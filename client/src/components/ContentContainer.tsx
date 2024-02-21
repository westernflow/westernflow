import React, { ReactNode } from 'react';

interface ContentContainerProps {
    children: ReactNode;
}

export default function ContentContainer({ children }: ContentContainerProps) {
    return <div className="mx-auto max-w-7xl sm:px-6 lg:px-8 min-h-screen"> {children} </div>;
}
