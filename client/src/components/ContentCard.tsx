import React from 'react';

interface ContentCardProps {
	children: React.ReactNode;
	classNames?: string;
}

export function ContentCard({ children, classNames }: ContentCardProps) {
	return <div className={"bg-gray-100 shadow-lg rounded-lg p-6 " + (classNames ? classNames : "")}>{children}</div>;
}
