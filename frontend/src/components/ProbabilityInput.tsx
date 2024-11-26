import React from 'react';

interface ProbabilityInputProps {
  id: string;
  label: string;
  value: string;
  onChange: (value: string) => void;
}

export function ProbabilityInput({ id, label, value, onChange }: ProbabilityInputProps) {
  return (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-gray-700 mb-1">
        {label}
      </label>
      <input
        id={id}
        type="number"
        step="0.1"
        min="0"
        max="1"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
        placeholder="Enter value (0-1)"
      />
    </div>
  );
}